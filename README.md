# EbeeCleanArchitectureTemplate

A comprehensive .NET 8 Web API template implementing Clean Architecture principles with modern development practices.

## 🏗️ Architecture Overview

This template follows Clean Architecture principles, organizing code into distinct layers with clear separation of concerns:

### Project Structure
```
├── EbeeCleanArchitectureTemplate.API/          # Presentation Layer
├── EbeeCleanArchitectureTemplate.Application/  # Application Layer
├── EbeeCleanArchitectureTemplate.Domain/       # Domain Layer
└── EbeeCleanArchitectureTemplate.Infrastructure/ # Infrastructure Layer
```

### Domain Layer
- **Entities**: Core business entities (`ApplicationUser`, `BaseEntity`)
- **Enums**: Domain enumerations (`Roles`)
- **Models**: Domain models and value objects (`Result<T>`)

### Application Layer
- **Abstractions**: Interfaces and contracts (`IApplicationDbContext`)
- **Models**: Application-specific models (`PagedResult<T>`)
- **Utilities**: Application utilities (`Pagination`)

### Infrastructure Layer
- **Persistence**: Data access and Entity Framework configuration
- **Identity**: Authentication and authorization setup
- **Logging**: Serilog configuration

### API Layer
- **Controllers**: HTTP endpoints
- **Middleware**: Global exception handling
- **Configuration**: Swagger, DI container setup

## ✨ Features

### 🔐 Authentication & Authorization
- **JWT Bearer Authentication** with configurable expiry times
- **ASP.NET Core Identity** integration with role-based authorization
- **Fallback Authorization Policy** requiring authenticated users by default
- **Password Policy**: Enforced complexity requirements
  - Minimum 8 characters
  - Requires uppercase, lowercase, and digit
  - Unique email addresses required

### 📊 Database & Data Management
- **Entity Framework Core** with SQL Server provider
- **Code-First Migrations** support
- **Automatic Auditing**: Tracks `CreatedAt`, `CreatedBy`, `UpdatedAt`, `UpdatedBy`
- **Database Seeding**: Automatic role and user initialization
- **Generic Base Entity**: Provides common properties for all entities

### 🔄 Result Pattern
- **Generic Result<T>** for operation outcomes
- **HTTP Status Code** integration
- **Error Handling** with detailed error information
- **JSON Serialization** optimized with conditional properties

### 📄 Pagination
- **Generic Pagination Utility** for efficient data retrieval
- **PagedResult<T>** with comprehensive metadata:
  - Current page and page size
  - Total count and total pages
  - Start/end record calculations
  - Navigation properties (`HasNextPage`, `HasPreviousPage`)

### 🛡️ Error Handling
- **Global Exception Handler** with structured logging
- **Problem Details** standard for API errors
- **Serilog Integration** for comprehensive logging

### 📝 API Documentation
- **Swagger/OpenAPI** with JWT Bearer authentication support
- **XML Documentation** comments integration
- **Security Schemes** pre-configured

### ⚙️ Configuration
- **Strongly-typed Configuration** sections
- **Environment-specific** settings support
- **User Secrets** integration for development

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server (LocalDB, Express, or Full)
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
```bash
git clone https://github.com/ebeeraheem/EbeeCleanArchitectureTemplate
cd EbeeCleanArchitectureTemplate
```

2. **Update Configuration**
   
   Edit `appsettings.json` and configure:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your SQL Server connection string"
  },
  "SeedUser": {
    "FirstName": "Your First Name",
    "LastName": "Your Last Name", 
    "Email": "your-email@example.com",
    "Password": "YourS3cureP@ssw0rD"
  },
  "Jwt": {
    "Issuer": "Your API Name",
    "Audience": "https://yourdomain.com",
    "Key": "YourSup3rDup3rUltr@M3gaSecur3AwesomeKey123"
  }
}
```

3. **Run Database Migrations**
```bash
dotnet ef database update --project EbeeCleanArchitectureTemplate.Infrastructure
```

4. **Run the Application**
```bash
dotnet run --project EbeeCleanArchitectureTemplate.API
```

5. **Access Swagger UI**

Navigate to: `https://localhost:5001/swagger`

## 🔧 Configuration Details

### JWT Authentication
The template includes comprehensive JWT configuration:
- **Access Token Expiry**: 15 minutes (configurable)
- **Refresh Token Expiry**: 7 days (configurable)
- **Symmetric Key Encryption**: HS256 algorithm
- **Issuer/Audience Validation**: Enabled

### Logging with Serilog
- **Console Logging**: Development-friendly output
- **File Logging**: Structured JSON logs with hourly rotation
- **Log Levels**: Configurable minimum levels
- **Request Logging**: HTTP request/response logging

### Database Seeding
Automatic initialization includes:
- **Roles**: `Admin`, `User` (from `Roles` enum)
- **Admin User**: Created from `SeedUser` configuration
- **Idempotent**: Safe to run multiple times

## 🏛️ Architecture Patterns

### Result Pattern Implementation
```csharp
// Success response
var result = Result<User>.Success(user, 200);

// Error response
var error = new Error("User not found");
var result = Result<User>.Failure(404, error);
```

### Pagination Usage
```csharp
var pagedResult = await Pagination.PaginateAsync(
    query: usersQuery,
    pageNumber: 1,
    pageSize: 10
);

// Or using extension method
var pagedResult = await usersQuery.PaginateAsync(
    pageNumber: 1,
    pageSize: 10
);
```

### Base Entity Inheritance
```csharp
public class MyEntity : BaseEntity
{
    public string Name { get; set; }
    // CreatedAt, CreatedBy, UpdatedAt, UpdatedBy inherited
}
```

## ⚠️ Important Notes

### Treat Warnings as Errors
This template has **TreatWarningsAsErrors** enabled for both Debug and Release configurations, ensuring high code quality. The following warnings are suppressed:
- `1701`, `1702`: Version conflicts
- `1591`: Missing XML documentation

### Security Considerations
- **Change default JWT key** in production
- **Use Azure Key Vault** or similar for secrets management
- **Update seed user credentials** before deployment
- **Review password policies** for your requirements

### Production Deployment
Before deploying to production:
1. Update connection strings
2. Configure proper logging sinks
3. Set up application insights or monitoring
4. Review and update CORS policies if needed
5. Configure proper SSL certificates

## 📁 Key Files

| File | Purpose |
|------|---------|
| `Program.cs` | Application entry point and DI configuration |
| `GlobalExceptionHandler.cs` | Centralized error handling |
| `ApplicationDbContext.cs` | EF Core context with auditing |
| `InfrastructureExtensions.cs` | Infrastructure service registration |
| `Result.cs` | Result pattern implementation |
| `PagedResult.cs` | Pagination response model |
| `DbInitializer.cs` | Database seeding logic |

## 📦 Dependencies

### Core Packages
- **Microsoft.AspNetCore.App** (Framework)
- **Microsoft.EntityFrameworkCore.SqlServer** (Data Access)
- **Microsoft.AspNetCore.Identity.EntityFrameworkCore** (Identity)
- **Microsoft.AspNetCore.Authentication.JwtBearer** (JWT Auth)
- **Serilog.AspNetCore** (Logging)
- **Swashbuckle.AspNetCore** (API Documentation)

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Ensure all tests pass
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.

## 🙋‍♂️ Support

For questions or support, please open an issue on the GitHub repository.

---

**Happy Coding!** 🚀