using System.Security.Claims;

namespace CleanSample.Presentation.FunctionApp.Middleware;

/// <summary>
///     Holds the authenticated user principal
///     for the request along with the
///     access token they used.
/// </summary>
public class JwtPrincipalFeature(ClaimsPrincipal principal, string accessToken)
{
    public ClaimsPrincipal Principal { get; } = principal;

    /// <summary>
    ///     The access token that was used for this
    ///     request. Can be used to acquire further
    ///     access tokens with the on-behalf-of flow.
    /// </summary>
    public string AccessToken { get; } = accessToken;
}