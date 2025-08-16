using GameStore.Endpoints;
using GameStore.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IGameRepository, InMemRepository>();

// SQL Server Inside the Container:
// SQL Server is running inside the Docker container, listening on port 1433 (the default port for SQL Server).

// Port Mapping:
// When you run 'docker run -p 15500:1433', you're telling Docker to map port 1433 inside the container
// to port 15500 on your local machine.
// This allows you to connect to the SQL Server instance running inside the container by using 'localhost,15500'
// on your local machine.

// Connecting via VS Code:
// In VS Code, when you connect to the database, you specify the following details:
//   - Server: localhost,15500 (this tells VS Code to connect to your local machine’s port 15500,
//     which is forwarded to SQL Server's port 1433 inside the container).
//   - Username: SA (this is the default SQL Server administrator user).
//   - Password: The password you set when creating the container (e.g., TestPassword132).

// VS Code Connects to SQL Server:
// VS Code sends the connection request to 'localhost,15500', and Docker forwards this request
// to the SQL Server instance inside the container via port 1433.
// SQL Server authenticates using the SA credentials, and if successful, you’re connected.



// Before using the connection string:
// - VS Code connects to the database directly by specifying the server (localhost,15500), username (SA), and password.
// - This is done manually in the VS Code interface, and it is mainly used for querying and managing the database interactively
// After using the connection string in the application:
// - Your application (like a C# app) needs to connect to the database programmatically.
// - The connection string in `appsettings.json` provides the necessary configuration to establish the connection automatically.
// - The connection string includes the server, port, database, username, and password required for your app to interact with the database.
// - The app reads this connection string and uses it to make a connection to SQL Server running inside the Docker container.



// GameStoreContext is the name of the DbContext class used in Entity Framework Core to interact with the database in a C# application.(see the appsettings.json)
builder.Configuration.GetConnectionString("GameStoreContext");

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
