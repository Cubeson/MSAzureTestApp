using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
namespace Test.FunctionApp1;

public class ExampleFunctionClass
{
    [Function("HttpExample")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}