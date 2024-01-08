# Anime Ratings Web Application

## Introduction
This web application is designed to provide users with an engaging platform to rate and review anime. The application has recently transitioned from using a JSON-based storage mechanism to a robust SQL database, enhancing its scalability and performance.

## Features
- **Anime Listing**: Displays a list of animes with options for users to view more details.
- **Ratings**: Users can rate animes which are then updated in real-time in the database.
- **Reviews**: Users can add reviews for each anime.
- **Data Migration**: A one-time migration from JSON to SQL database has been implemented to transfer existing data without loss.

## Tech Stack
- **Frontend**: Blazor Server
- **Backend**: ASP.NET Core, Entity Framework Core
- **Database**: SQL Server hosted on Azure

## Configuration
The application leverages Azure services, including Azure App Service for hosting and Azure SQL Database for data persistence. It utilizes Azure Key Vault to securely manage the database connection string.

## Database Transition
The transition from JSON to SQL database is a significant upgrade. Initially, the application used a JSON file (`animes.json`) to store and retrieve data. With the scaling requirements, a decision was made to migrate to a SQL database, which now handles all CRUD operations and provides a more stable and persistent storage solution.

## Migration Logic
The migration logic is encapsulated within a hosted service (`DataMigrationHostedService`) that runs once at the startup of the application. It checks for the existence of data in the database and, if not present, migrates the data from the JSON file.

## Environment Setup
Before running the application, ensure that:
- The Azure SQL Database is set up with the correct schema.
- The Azure Key Vault has the SQL Database connection string.
- The application settings in Azure App Service have the Key Vault URI configured.

## Local Development
For local development:
- Update `appsettings.json` with the local database connection string.
- Use the `appsettings.Development.json` to override settings for the development environment.

## Running the Application
To run the application locally, use the `dotnet run` command within the project directory. For production, push the code to the Azure App Service through your preferred CI/CD pipeline.

## Logging
Logging is configured to capture essential information and errors. In Azure, the Log stream service can be used to view real-time logs.

## Future Enhancements
Planned future enhancements include:
- User authentication and authorization.
- Social sharing of ratings and reviews.
- Advanced anime search and filtering capabilities.

## Acknowledgements
Special thanks to all the contributors and users who have made this project a vibrant platform for anime enthusiasts.

