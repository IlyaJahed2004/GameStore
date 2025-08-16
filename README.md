# Game Store

## Starting SQL Server
```PowerShell
# Set the SA (system administrator) password
$sa_password = "YourStrong!Pass1"

# Run the SQL Server container
docker run \
-e "ACCEPT_EULA=Y"\                   # Accepts SQL Server license terms
-e "MSSQL_SA_PASSWORD=$sa_password"\  # Sets the SA password using PowerShell variable
-p 15500:1433\                         # Maps host port 15500 to container port 1433
-v sqlvolume:/var/opt/mssql \          # Mounts a Docker volume to persist database files
-d --rm --name mssql                   # Runs container in background and removes it when stopped
mcr.microsoft.com/mssql/server:2022-latest  # Official SQL Server 2022 image


#one line command: 
docker run \
-e "ACCEPT_EULA=Y" \
-e "MSSQL_SA_PASSWORD=$sa_password" \
-p 15500:1433 \
-v sqlvolume:/var/opt/mssql \
-d --rm --name mssql \
mcr.microsoft.com/mssql/server:2022-latest


```

## Setting the connection string to secret manager:
```Powershell:
#firstly we should initialize user-secrets: 
dotnet user-secrets init

# then :
dotnet user-secrets set "ConnectionStrings:GameStoreContext" "Server=localhost,15500;Database=GameStore;User Id=SA;Password=$sa_password;TrustServerCertificate=True"

```


// ---------------------- Notes on Secret Manager ----------------------
// Why use it?
//   - Avoids hardcoding DB passwords or storing them in appsettings.json.
//
// Steps:
//   1. Initialize secrets for the project:
//        dotnet user-secrets init
//   2. Save the connection string securely:
//        dotnet user-secrets set "ConnectionStrings:GameStoreContext" 
//        "Server=localhost,15500;Database=GameStore;User Id=SA;Password=YourStrong!Pass1;TrustServerCertificate=True"
//
// Usage in app:
//   - builder can now fetch the connection string from Secret Manager.
//   - It is injected into IConfiguration, so the app can access it through:
//        builder.Configuration.GetConnectionString("GameStoreContext");
//
// Result:
//   - Keeps sensitive info out of source code and config files.
//   - Still accessible at runtime via IConfiguration.
// ---------------------------------------------------------------------
