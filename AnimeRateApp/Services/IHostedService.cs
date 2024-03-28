using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AnimeRateApp.Services
{
    // This class implements the IHostedService interface, which allows for background operations on app start and stop.
    public class DataMigrationHostedService : IHostedService
    {
        // The service provider for dependency injection.
        private readonly IServiceProvider _serviceProvider;

        // Constructor that takes the service provider, allowing services to be resolved.
        public DataMigrationHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        // The method to be called when the application starts. It can be used to run startup tasks.
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Creating a scope to resolve scoped services
            using (var scope = _serviceProvider.CreateScope())
            {
                // Resolving the service to handle JSON data migration
                var jsonFileAnimesService = scope.ServiceProvider.GetRequiredService<JsonFileAnimesService>();

                // Uncomment the line below to run the migration when this service starts
                // await jsonFileAnimesService.MigrateJsonDataToDatabaseAsync();
            }
        }

        // The method to be called when the application stops. It can be used to run cleanup tasks.
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}