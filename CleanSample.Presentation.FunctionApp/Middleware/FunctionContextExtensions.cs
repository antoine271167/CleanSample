using System.Net;
using System.Reflection;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace CleanSample.Presentation.FunctionApp.Middleware;

internal static class FunctionContextExtensions
{
    internal static async Task SetHttpResponseStatusCodeAsync(this FunctionContext context, HttpStatusCode statusCode)
    {
        var req = await context.GetHttpRequestDataAsync();
        if (req == null)
        {
            throw new InvalidOperationException("Unable to retrieve HttpRequestData");
        }

        var res = req.CreateResponse();
        res.StatusCode = statusCode;
    }

    internal static HttpRequestData? GetHttpRequestData(this FunctionContext context)
    {
        var keyValuePair = context.Features.SingleOrDefault(f => f.Key.Name == "IFunctionBindingsFeature");
        var functionBindingsFeature = keyValuePair.Value;
        var type = functionBindingsFeature.GetType();
        var inputData =
            type.GetProperties().Single(p => p.Name == "InputData").GetValue(functionBindingsFeature) as
                IReadOnlyDictionary<string, object>;
        return inputData?.Values.SingleOrDefault(o => o is HttpRequestData) as HttpRequestData;
    }


    public static MethodInfo GetTargetFunctionMethod(this FunctionContext context)
    {
        // More terrible reflection code.
        // Would be nice if this was available out of the box on FunctionContext

        // This contains the fully qualified name of the method
        // E.g. IsolatedFunctionAuth.TestFunctions.ScopesAndAppRoles
        var entryPoint = context.FunctionDefinition.EntryPoint;

        var assemblyPath = context.FunctionDefinition.PathToAssembly;
        var assembly = Assembly.LoadFrom(assemblyPath);
        var typeName = entryPoint.Substring(0, entryPoint.LastIndexOf('.'));
        var type = assembly.GetType(typeName);
        var methodName = entryPoint.Substring(entryPoint.LastIndexOf('.') + 1);
        var method = type.GetMethod(methodName);
        return method;
    }
}