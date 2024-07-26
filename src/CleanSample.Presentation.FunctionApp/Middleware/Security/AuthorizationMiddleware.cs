using System.Net;
using System.Reflection;
using System.Security.Claims;
using CleanSample.Presentation.FunctionApp.Authorization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace CleanSample.Presentation.FunctionApp.Middleware.Security;

public class AuthorizationMiddleware : IFunctionsWorkerMiddleware
{
    private const string _scopeClaimType = "http://schemas.microsoft.com/identity/claims/scope";

    public async Task Invoke(
        FunctionContext context,
        FunctionExecutionDelegate next)
    {
        var principalFeature = context.Features.Get<JwtPrincipalFeature>();
        if (!AuthorizePrincipal(context, principalFeature!.Principal))
        {
            await context.SetHttpResponseStatusCodeAsync(HttpStatusCode.Forbidden);
            return;
        }

        await next.Invoke(context);
    }

    private static bool AuthorizePrincipal(FunctionContext context, ClaimsPrincipal principal)
    {
        // This authorization implementation was made
        // for Azure AD. Your identity provider might differ.

        if (principal.HasClaim(c => c.Type == _scopeClaimType))
        {
            // Request made with delegated permissions, check scopes and user roles
            return AuthorizeDelegatedPermissions(context, principal);
        }

        // Request made with application permissions, check app roles
        return AuthorizeApplicationPermissions(context, principal);
    }

    private static bool AuthorizeDelegatedPermissions(FunctionContext context, ClaimsPrincipal principal)
    {
        var targetMethod = context.GetTargetFunctionMethod();

        var (acceptedScopes, acceptedUserRoles) = GetAcceptedScopesAndUserRoles(targetMethod);

        var userRoles = principal.FindAll(ClaimTypes.Role);
        var userHasAcceptedRole = userRoles.Any(ur => acceptedUserRoles.Contains(ur.Value));

        // Scopes are stored in a single claim, space-separated
        var callerScopes = (principal.FindFirst(_scopeClaimType)?.Value ?? "")
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var callerHasAcceptedScope = callerScopes.Any(acceptedScopes.Contains);

        // This app requires both a scope and user role
        // when called with scopes, so we check both
        return userHasAcceptedRole && callerHasAcceptedScope;
    }

    private static bool AuthorizeApplicationPermissions(FunctionContext context, ClaimsPrincipal principal)
    {
        var targetMethod = context.GetTargetFunctionMethod();

        var acceptedAppRoles = GetAcceptedAppRoles(targetMethod);
        var appRoles = principal.FindAll(ClaimTypes.Role);
        var appHasAcceptedRole = appRoles.Any(ur => acceptedAppRoles.Contains(ur.Value));
        return appHasAcceptedRole;
    }

    private static (List<string> scopes, List<string> userRoles) GetAcceptedScopesAndUserRoles(MemberInfo targetMethod)
    {
        var attributes = GetCustomAttributesOnClassAndMethod<AuthorizeAttribute>(targetMethod);
        var scopes = attributes.SelectMany(a => a.Scopes).Distinct().ToList();
        var userRoles = attributes.SelectMany(a => a.UserRoles).Distinct().ToList();
        return (scopes, userRoles);
    }

    private static List<string> GetAcceptedAppRoles(MemberInfo targetMethod)
    {
        var attributes = GetCustomAttributesOnClassAndMethod<AuthorizeAttribute>(targetMethod);
        return attributes.SelectMany(a => a.AppRoles).Distinct().ToList();
    }

    private static List<T> GetCustomAttributesOnClassAndMethod<T>(MemberInfo targetMethod)
        where T : Attribute
    {
        var methodAttributes = targetMethod.GetCustomAttributes<T>();
        var classAttributes = targetMethod.DeclaringType!.GetCustomAttributes<T>();
        return methodAttributes.Concat(classAttributes).ToList();
    }
}