# Use the .NET SDK image as a base
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory inside the container
WORKDIR /"Project Management System"

# Copy the ASP.NET project files into the container
COPY . .

# Restore dependencies and build the project
RUN dotnet restore

# Publish the ASP.NET application
RUN dotnet publish -c Release -o out

# Expose the port the application listens on
EXPOSE 44324

# Set the entry point for the container
ENTRYPOINT ["dotnet", "Project Management System/bin/Release/net6.0/Project Management System.dll"]
