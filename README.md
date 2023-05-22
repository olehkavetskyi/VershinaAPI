# Vershina API

Vershina is a web shop created in order to learn Angular and ASP.NET and get to know Redis that is used for storing information elements in a basket.

## Installation
To install the API repository, follow these steps:

1. Clone the repository to your local machine using Git.

```bash
git clone https://github.com/olehkavetskyi/VershinaAPI
```

2. Install the .NET 6 on your machine if it is not already installed.

3. Navigate to the root of the repository using the command prompt.

4. Run the command dotnet restore to install the dependencies.

.NET Server Side Repository

Installation
To install the .NET server-side repository, you can follow these steps:

Clone the repository to your local machine using Git.
Install .NET Core SDK 6.0 or higher if you haven't already.
Install Redis on your local machine or set up a Redis instance in Azure.
Create Azure databases (SQL Server or Cosmos DB) and obtain the connection strings.
Create a secrets.json file in the root directory of the repository and add the Redis and Azure database connection strings in the following format:
json
Copy code
```json
{
  "Redis": {
    "ConnectionString": "your_redis_connection_string"
  },
  "Sql": {
    "ConnectionString": "your_sql_connection_string"
  },
  "CosmosDb": {
    "ConnectionString": "your_cosmos_db_connection_string",
    "DatabaseName": "your_cosmos_db_database_name"
  }
}
```
Run the command dotnet run to start the application.
## Usage
To use the VershinaAPI repository, you can follow these steps:

1. Send HTTP requests to the server using a tool such as Postman or cURL.
2. The server will interact with the Redis cache and Azure databases to handle the requests and return responses.
3. Send HTTP requests to the server using UI part of the appication

## Deployment
To deploy the VershinaAPI repository to Azure, you can follow these steps:

1. Create an Azure App Service.
2. Set up deployment from your Git repository.
3. Configure the App Service settings to include the Redis and Azure database connection strings.
4. Deploy the code to the App Service using your preferred method (e.g. Git deployment, Visual Studio publish).
5. Test the application by sending HTTP requests to the App Service endpoint.

## Deployment the full project

To deploy the VershinaAPI repository to Azure, you can follow these steps:

1. Create an Azure App Service.
2. Set up deployment from your Git repository.
3. Configure the App Service settings to include the Redis and Azure database connection strings.
4. Build the UI part of the app into API.wwwroot folder
5. Add these fragments of code into Program.cs 
  ```cs
  app.UseStaticFiles(new StaticFileOptions
  {
      FileProvider = new PhysicalFileProvider(
          Path.Combine(Directory.GetCurrentDirectory(), "Content")), RequestPath = "/Content"
  });
  ```
  
  ```cs
  app.MapFallbackToController("Index", "Fallback");
  ```
  6. Create FallbackController.cs copy and past this code into it
  ```cs
  using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class FallbackController : Controller
{
    public IActionResult Index()
    {
        return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
    }
}
  ```
5. Deploy the code to the App Service using your preferred method (e.g. Git deployment, Visual Studio publish).

## Links

In order to see UI part follow [this](https://github.com/olehkavetskyi/VershinaUI) link
