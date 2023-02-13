using BlazorLiveDemo.Server.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorLiveDemo.Server.DataAccess;

public class PeopleContext : DbContext
{
    public DbSet<PersonModel> People { get; set; }

    public PeopleContext(DbContextOptions options) : base(options)
    {
        
    }
}