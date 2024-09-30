# Core Entities Admin API

This is the backend of the Core Entities Admin application, developed using .NET 7 and SQL Server. This API allows users to manage entities, and employees, and perform CRUD operations, with authentication and role-based access control using **ASP.NET Core Identity** and **JWT**.

## Requirements and Configuration

Make sure to have the following installed:
- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- SQL Server or an Azure SQL Database

## Installation and Execution

1. Clone the repository: `git clone <URL>`
2. Install dependencies: `dotnet restore`
3. Configure environment variables in the `appsettings.json` and `appsettings.Development.json` files.
4. Run the application locally: `dotnet run`
5. To publish and deploy to an Azure Web App, follow the [Azure Web App deployment guide](https://learn.microsoft.com/en-us/azure/app-service/quickstart-dotnetcore).

## Database Migration

To run the database migrations and create the necessary tables in the SQL Server or Azure SQL Database:

1. Ensure the connection string is set in `appsettings.json` or `appsettings.Development.json`.
2. Run the following command to apply migrations: `dotnet ef database update`

If the migrations were already created, you can add new ones with: `dotnet ef migrations add <MigrationName>`

This will automatically create the necessary tables and seed the default roles and users.

## Architecture and Folder Structure

The project follows a clean architecture structure to maintain scalability and separation of concerns. Key components are organized as follows:

- `/Controllers`: Handles HTTP requests and returns responses.
- `/Models`: Defines the structure of data used in the application.
- `/Repositories`: Encapsulates data access and provides methods to interact with the database.
- `/Services`: Contains business logic and handles tasks related to entities and employees.
- `/Data`: Database context and EF Core configurations.
- `/Migrations`: Auto-generated migrations for updating the database.

## Authentication and Authorization

The application uses **ASP.NET Core Identity** to handle user authentication and role management. 

- **IdentityRole**: This is used to define roles like **Administrator** and **User**. Roles are assigned to users to restrict access to certain functionalities.
- **JWT (JSON Web Token)**: The API uses JWT for secure authentication. Upon successful login, a JWT token is generated and returned to the client. The client then sends the token in the `Authorization` header for each subsequent request. The API validates the token to authenticate users and check their roles.

## Environment Variables

To run this project, you will need to configure the following environment variables:

- `ConnectionStrings:DefaultConnection`: Your database connection string.
- `Jwt:Key`: The secret key for JWT authentication.
- `Jwt:Issuer`: Issuer for the JWT.
- `Jwt:Audience`: Audience for the JWT.

## API Reference

### Create Entity

**POST** `/entity/create`

| Parameter    | Type     | Description                  |
| ------------ | -------- | ---------------------------- |
| `name`       | `string` | **Required**. Entity name     |
| `description`| `string` | **Required**. Entity description |
| `address`    | `string` | **Required**. Entity address  |
| `email`      | `string` | **Required**. Entity email    |
| `phone`      | `string` | Optional. Entity phone        |

### Get All Entities

**GET** `/entity/all`

### Get Entity by ID

**GET** `/entity/{id}`

| Parameter | Type     | Description                       |
| --------- | -------- | --------------------------------- |
| `id`      | `number` | **Required**. Entity ID to fetch  |

### Delete Entity

**DELETE** `/entity/delete/{id}`

| Parameter | Type     | Description                       |
| --------- | -------- | --------------------------------- |
| `id`      | `number` | **Required**. Entity ID to delete |

## Deploy to Azure

### Deploy to Azure Web App

To deploy the application to Azure Web App, we can use vs code directly.

### Deploy Database to Azure

1. Create an Azure SQL Database.
2. Update the connection string in the Azure App Settings.
3. Run the database migrations: `dotnet ef database update`

## One-to-Many Relationship

The API is designed with a one-to-many relationship between entities and employees. This allows each entity to manage its employees independently, ensuring a scalable and flexible system. 

Below is a brief explanation of the database structure:

Entity represents organizations or administrative units in the system.
Employee represents individuals who belong to an entity.

```bash

+----------------+         1      *        +----------------+
|     Entity     |------------------------>|    Employee    |
+----------------+                         +----------------+
| Id             |                         | Id             |
| Name           |                         | FirstName      |
| Description    |                         | LastName       |
| Address        |                         | Position       |
| Email          |                         | Email          |
| Phone          |

```
