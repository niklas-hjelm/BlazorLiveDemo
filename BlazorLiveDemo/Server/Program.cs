using BlazorLiveDemo.Server.DataAccess;
using BlazorLiveDemo.Server.Hubs;
using BlazorLiveDemo.Shared;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorPages();

builder.Services.AddDbContext<PeopleContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("PeopleDb");
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<PeopleRepository>();

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.MapGet("/allPeople", (PeopleRepository repo) =>
{
    return Results.Ok(repo.GetAllPeople());
});

app.MapPost("/addPerson", (PeopleRepository repo, PersonDto person) =>
{
    repo.Add(person);
    return Results.Ok("Added person");
});

app.MapHub<ChatHub>("/hubs/ChatHub");

app.MapFallbackToFile("index.html");

app.Run();
