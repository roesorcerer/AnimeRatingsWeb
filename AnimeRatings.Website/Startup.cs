using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using AnimeRatings.Website.Services;
using AnimeRatings.Website.Models;
using System.Text.Json;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using AnimeRatings.Website.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace AnimeRatings.Website
{
    // The Startup class configures services and the app's request pipeline
    public class Startup
    {
        // Constructor to initialize the configuration settings
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // Property to hold the configuration settings
        public IConfiguration Configuration { get; }

        // ConfigureServices is used to register services for the application in the dependency injection container
        public void ConfigureServices(IServiceCollection services)
        {
            // Azure Key Vault configuration
            var keyVaultUri = Configuration["ConnectionStrings:KeyVaultUri"];
            if (!string.IsNullOrEmpty(keyVaultUri))
            {
                // Create a SecretClient to access Azure Key Vault
                var secretClient = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());

                // Retrieve the database connection string stored in Azure Key Vault
                var connectionStringSecret = secretClient.GetSecret("ConnectionString");
                var connectionString = connectionStringSecret.Value.Value;

                // Configure DbContext with SQL Server using the connection string from Key Vault
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString), ServiceLifetime.Scoped);
            }
            else
            {
                // Fallback to local configuration if Key Vault is not configured
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);
            }

            // Register the DataMigrationHostedService as a hosted service
            services.AddHostedService<DataMigrationHostedService>();

            // Register JsonFileAnimesService with scoped lifetime for handling JSON file operations
            services.AddScoped<JsonFileAnimesService>();

            // Add support for Razor Pages
            services.AddRazorPages();

            // Add support for server-side Blazor
            services.AddServerSideBlazor();

            // Add MVC controllers to the service collection
            services.AddControllers();
        }
        // Configure is used to set up the request pipeline for the application
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            // Check if the environment is Development for detailed exception pages
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Use exception handler and HSTS in production
                app.UseExceptionHandler("/Error");
                app.UseHsts(); // Note: The default HSTS value is 30 days. You may want to change this for production.
            }

            // Enforce HTTPS redirection
            app.UseHttpsRedirection();

            // Serve static files (like images, CSS, JavaScript)
            app.UseStaticFiles();

            // Enable routing for the application
            app.UseRouting();

            // Enable authorization
            app.UseAuthorization();

            // Configure the endpoints for the application
            app.UseEndpoints(endpoints =>
            {
                // Map Razor Pages
                endpoints.MapRazorPages();

                // Map controllers
                endpoints.MapControllers();

                // Map Blazor SignalR hub
                endpoints.MapBlazorHub();
            });
        }
    }
}
