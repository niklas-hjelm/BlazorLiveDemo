using System.ComponentModel.DataAnnotations;

namespace BlazorLiveDemo.Server.DataAccess.Models;

public class PersonModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
}
