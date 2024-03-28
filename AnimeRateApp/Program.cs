using AnimeRateApp.Client.Pages;
// Ensure that the 'Data' namespace exists in your project
// using AnimeRateApp.Client.Data; //add data page
using AnimeRateApp.Client.Components;
using System.Text;
using Microsoft.Extensions.DependencyInjection; // Ensure this namespace is included for IServiceCollection
using BootstrapBlazor;
using AnimeRateApp.Services;
using Microsoft.EntityFrameworkCore;
using AnimeRateApp.Data; // Ensure this namespace is included for BootstrapBlazor

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// add support for Server-Side Blazor
builder.Services.AddServerSideBlazor();

// ...

// Ensure that the BootstrapBlazor NuGet package is installed in your project
builder.Services.AddBootstrapBlazor();

// register a service with the application's dependency injection container as a singleton
//builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddHostedService<DataMigrationHostedService>();


builder.Services.AddHttpClient();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
// add the routing middleware
app.UseRouting();
// map the SignalR hub for Blazor Server-Side
app.MapBlazorHub();
// specify a fallback page that will be used when the application cannot find a matching endpoint
app.MapFallbackToPage("/_Host");
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(AnimeRateApp.Client._Imports).Assembly);

app.Run();
