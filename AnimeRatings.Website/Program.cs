using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AnimeRatings.Website
{
    // Program is the main entry point for the application
    public class Program
    {
        // Main method initializes and starts the web application
        public static void Main(string[] args)
        {
            // Build and run the web host
            CreateHostBuilder(args).Build().Run();
        }

        // CreateHostBuilder configures and initializes a new instance of IHostBuilder
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args) // Create a default builder with pre-configured defaults
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>(); // Specify the startup class to be used by the web host
                });
    }
}
