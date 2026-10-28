# Employee Management API

A backend REST API built with **ASP.NET Core (.NET 10)** and **C#**. It supports user registration and login with JWT authentication, and secured CRUD operations for employees, using Entity Framework Core and SQL Server.

> **Status:** Work in progress 🚧. Core features are working; validation, tests and more endpoints are still being added.

## Features

- User registration and login
- JWT authentication and authorization
- Employee management: create, read, update, delete
- Entity Framework Core with code-first migrations
- SQL Server / LocalDB integration
- Service layer with interfaces and dependency injection
- DTOs and a generic response wrapper (`ResponseResult<T>`)

## Tech Stack

| Area | Technology |
|---|---|
| Language | C# |
| Framework | ASP.NET Core Web API, .NET 10 |
| ORM | Entity Framework Core |
| Database | SQL Server / LocalDB |
| Authentication | JWT Bearer tokens |
| API testing | Postman |

## System Design

### Architecture

```mermaid
flowchart LR
    Client[Client: Postman] --> Controllers
    Controllers --> Services[Services via interfaces]
    Services --> DbContext[AppDbContext - EF Core]
    DbContext --> DB[(SQL Server)]
    Controllers -. validates JWT .- Auth[JWT Authentication]
```

### Request flow

1. The client registers or logs in and receives a JWT.
2. The client sends the token in the `Authorization: Bearer <token>` header.
3. A controller marked `[Authorize]` receives the request and passes DTOs to the service.
4. The service contains the business logic and uses `AppDbContext` to read or write data.
5. The result is wrapped in `ResponseResult<T>` and returned as JSON.

### Project structure

```
Controllers/      HTTP endpoints (Auth, Employee)
Services/         Service implementations
IServices/        Service interfaces (for dependency injection)
Dto/              Request and response models
Entities/         Database entities
Data/             AppDbContext
Migrations/       EF Core migrations
GenericResponse/  Standard response wrapper
sql/              Schema script and example queries
```

### Design decisions

- **Interfaces and dependency injection** keep controllers independent of implementations and make services easy to test.
- **DTOs** keep database entities from being exposed directly to clients.
- **Code-first migrations** keep the database schema versioned together with the code.
- **JWT** keeps the API stateless: each request carries its own proof of identity.

## Database Design

SQL Server database managed with EF Core code-first migrations.

### Entity relationship diagram

```mermaid
erDiagram
    EMPLOYEES {
        uniqueidentifier Id PK
        nvarchar Name
        datetime2 CreateDate
        datetime2 LastModifiedDate
        date DOB
        nvarchar Position
        nvarchar Department
        nvarchar EmailAddress
    }
    ACOUNT_USERS {
        uniqueidentifier Id PK
        nvarchar Name
        nvarchar Email
        nvarchar Password
    }
```

### Tables

| Table | Purpose |
|---|---|
| `Employees` | Employee records managed through the Employee API |
| `AcountUsers` | Login accounts used for registration and JWT authentication |

### Notes

- Both tables use a `Guid` primary key.
- The two tables are independent: login accounts are not linked to employee records.
- `Employees.Department` is currently a plain text column.
- The full SQL is in [`sql/schema.sql`](sql/schema.sql), with example queries in [`sql/queries.sql`](sql/queries.sql).

### Planned database improvements

- Add a `Departments` table and replace `Employees.Department` with a `DepartmentId` foreign key
- Add a `Salaries` table linked to `Employees`
- Add a unique constraint on email addresses
- Rename `AcountUsers` to `Users`

## API Endpoints

### Employee (requires a valid JWT)

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/Employee/GetAllEmployees` | List all employees |
| GET | `/api/Employee/GetEmployeeById/{id}` | Get one employee |
| POST | `/api/Employee/CreateEmployee` | Create an employee |
| PUT | `/api/Employee/UpdateEmployee` | Update an employee |
| DELETE | `/api/Employee/DeleteEmployee?id={id}` | Delete an employee |

### Auth

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/Auth/<register-route>` | Register a user |
| POST | `/api/Auth/<login-route>` | Log in and receive a JWT |

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server or SQL Server LocalDB

### Run locally

```bash
git clone https://github.com/affansafi1221-byte/EmployeeManagementAPI.git
cd EmployeeManagementAPI

# create the database from the migrations
dotnet ef database update

# run the API
dotnet run
```

Then call the API from Postman. Log in first, then send the returned token as a Bearer token on the Employee endpoints.

## Roadmap

- [ ] Link `Employee` to `Department` with a foreign key
- [ ] Return proper HTTP status codes (404, 400) instead of always 200
- [ ] Input validation
- [ ] Unit tests with xUnit
- [ ] Pagination and search for employees
- [ ] Postman collection and screenshots

## Learning Goals

- Understand REST API development
- Practice authentication and authorization
- Work with databases using Entity Framework Core
- Learn API testing with Postman

## Disclaimer

This is a learning project and is not intended for production use yet.
