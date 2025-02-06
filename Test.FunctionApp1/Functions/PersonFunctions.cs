using System.Net;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Test.FunctionApp1.DB;
using Microsoft.Azure.Functions.Worker;

namespace Test.FunctionApp1.Functions;
public class PersonFunctions
{
    private readonly AppDbContext _context;
    private readonly ILogger _logger;

    public PersonFunctions(AppDbContext context, ILogger<PersonFunctions> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [Function("GetPersons")]
    public async Task<HttpResponseData> GetPersons(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "persons")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _logger.LogInformation("Fetching all persons from the database.");

        var persons = await _context.Persons.ToListAsync();

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(persons);
        return response;
    }
    
    [Function("AddPerson")]
    public async Task<HttpResponseData> AddPerson(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "persons")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _logger.LogInformation("Adding a new person to the database.");

        var person = await req.ReadFromJsonAsync<Person>();
        _context.Persons.Add(person);
        await _context.SaveChangesAsync();

        var response = req.CreateResponse(HttpStatusCode.Created);
        await response.WriteAsJsonAsync(person);
        return response;
    }
}