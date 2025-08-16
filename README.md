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
docker run `
-e "ACCEPT_EULA=Y" `
-e "MSSQL_SA_PASSWORD=$sa_password" `
-p 15500:1433 `
-v sqlvolume:/var/opt/mssql `
-d --rm --name mssql `
mcr.microsoft.com/mssql/server:2022-latest


# ------------------------- Notes -------------------------

# About port:
# Maps host port 15500 to container port 1433:
# - 1433: the port SQL Server listens to inside the container (fixed)
# - 15500: the port on your Windows host; your app connects to this port
# Docker automatically forwards traffic from host 15500 → container 1433


# Volume Notes:
# - The -v sqlvolume:/var/opt/mssql option mounts a Docker-managed volume.
# - "sqlvolume" is created and stored outside the container on the host machine.
# - Inside the container, it appears as /var/opt/mssql (SQL Server's data directory).
# - Databases persist even if the container is removed or recreated.
# - Without -v, all data would be lost when the container is deleted.

# Image Pulling Notes:
# - When running 'docker run' or 'docker pull', Docker first checks if the image exists locally.
# - If not found, it downloads all layers of the image from the registry (e.g., mcr.microsoft.com).
# - Layers include the base OS (Ubuntu 22.04), SQL Server binaries, configuration files, etc.
# - The image is stored locally on the host machine, not installed system-wide.
# - Running 'docker run' creates a new container from the image:
#     - Mounts volumes for persistent data
#     - Maps ports (e.g., 1433:1433)
#     - Starts SQL Server inside an isolated container environment
# - Pulling the image does not install SQL Server globally.
# - Subsequent runs use the local image, so no download is needed again.

# Additional Notes:
# - The SA password must meet SQL Server requirements: at least 8 characters, including uppercase, lowercase, number, and symbol.
# - The container will be accessible on localhost:1433.
# - PowerShell is recommended over CMD for running Docker commands because it supports variables, better line continuation, and scripting.
