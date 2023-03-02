using Home_Server.Authentication;
using Home_Server.Data;
using Home_Server.Models;
using Home_Server.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using MySqlConnector;

DBConnection connector = new DBConnection("localhost", "homeserverdb");
var dbCon = await connector.Connect();

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, dbCon);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

static void ConfigureServices(IServiceCollection service, MySqlConnection dbCon)
{
    // Add services to the container.
    service.AddAuthenticationCore();
    service.AddRazorPages(); 
    service.AddServerSideBlazor();
    service.AddScoped<ProtectedSessionStorage>();
    service.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
    service.AddSingleton<UserAccountService>();
    service.AddSingleton<UserService>(us => new UserService(dbCon));    // Register Service
}
