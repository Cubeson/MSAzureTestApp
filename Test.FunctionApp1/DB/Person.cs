namespace Test.FunctionApp1.DB;
using System.ComponentModel.DataAnnotations;

public class Person
{
    [Key] public int Id { get; init; }
    [MaxLength(40)] public string FirstName { get; set; } = "";
    [MaxLength(40)] public string LastName { get; set; } = "";
}