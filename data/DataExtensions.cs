using Microsoft.EntityFrameworkCore;

namespace GameStore.data;

public static class DataExtensions
{
    public static void InitializeDb(this IServiceProvider serviceProvider)   //in the program the app.services is the class which implements the iserviceprovider
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }
}

// --- DataExtensions.InitializeDb ---
//
// This is an extension method for IServiceProvider that ensures the database is created
// and all EF Core migrations are applied on startup.
//
// 1. IServiceProvider Extension:
//    - The method is defined as `this IServiceProvider serviceProvider`, so it can be called
//      directly on any IServiceProvider instance (like app.Services.InitializeDb()).
//    - IServiceProvider = the interface through which the DI container provides services.
//
// 2. Create a Scope:
//    - DbContext has Scoped lifetime, meaning one instance per request.
//    - Program.cs or startup code runs outside a request, so we manually create a scope
//      to safely resolve DbContext.
//
// 3. Resolve DbContext:
//    - scope.ServiceProvider.GetRequiredService<GameStoreContext>() asks the DI container
//      to provide a ready-to-use instance of GameStoreContext.
//    - Dependencies are automatically injected, and an error is thrown if the service is not registered.
//
// 4. Apply Migrations:
//    - dbContext.Database.Migrate() applies any pending migrations to the database.
//    - If the database does not exist, it is automatically created.
//    - Ensures that the database schema is always up-to-date when the application starts.
//
// Usage Example:
//    app.Services.InitializeDb(); // called in Program.cs after building the app
