using System.Net;
using System.Reflection;
using Microsoft.Azure.Functions.Worker;

namespace CleanSample.Presentation.FunctionApp.Middleware.Security;

internal static class FunctionContextExtensions
{
    internal static async Task SetHttpResponseStatusCodeAsync(this FunctionContext context, HttpStatusCode statusCode)
    {
        var req = await context.GetHttpRequestDataAsync() ??
                  throw new InvalidOperationException("Unable to retrieve HttpRequestData");
        var res = req.CreateResponse();
        res.StatusCode = statusCode;
    }

    public static MethodInfo GetTargetFunctionMethod(this FunctionContext context)
    {
        var entryPoint = context.FunctionDefinition.EntryPoint;
        var assemblyPath = context.FunctionDefinition.PathToAssembly;
        var assembly = Assembly.Load(assemblyPath);
        var typeName = entryPoint[..entryPoint.LastIndexOf('.')];
        var type = assembly.GetType(typeName);
        var methodName = entryPoint[(entryPoint.LastIndexOf('.') + 1)..];
        var method = type!.GetMethod(methodName);
        return method!;
    }
}