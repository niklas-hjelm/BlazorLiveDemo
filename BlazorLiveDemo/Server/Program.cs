using BlazorLiveDemo.Server.DataAccess;
using BlazorLiveDemo.Server.Extensions;
using BlazorLiveDemo.Server.Hubs;
using BlazorLiveDemo.Server.Services;
using BlazorLiveDemo.Server.Services.Interfaces;
using BlazorLiveDemo.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorPages();

builder.Services.AddDbContext<PeopleContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("PeopleDb");
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IRepository<PersonDto>,PeopleRepository>();
builder.Services.AddScoped<IRepository<ChatMessageDto>,ChatRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

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

app.MapPersonEndpoints();

app.MapGet("/getAllChat", async (IRepository<ChatMessageDto> repo) =>
{
    return Results.Ok(await repo.GetAllAsync());
});

app.MapAuthEndpoints();

app.MapHub<ChatHub>("/hubs/ChatHub");

app.MapFallbackToFile("index.html");

app.Run();
