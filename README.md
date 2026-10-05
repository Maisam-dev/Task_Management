# CRM & Task Management API

A lightweight RESTful API built with **.NET 10** for customer management and task tracking, featuring role-based access control (RBAC).

*A frontend client for this service will be published in a separate repository.*

---

## Tech Stack

* **Framework**: .NET 10 (C# 12)
* **Database**: SQL Server
* **ORM**: Entity Framework Core 10 (`Microsoft.EntityFrameworkCore.SqlServer` v10.x)
* **Auth**: ASP.NET Core Identity & JWT Bearer (`Microsoft.AspNetCore.Authentication.JwtBearer` v10.x)
* **Utilities**: AutoMapper v16.x, Swashbuckle / OpenAPI v10.x
* **Containerization**: Docker & Docker Compose
* **Testing**: xUnit v2.x, Moq v4.x, FluentAssertions v6.x

## 🚀 Quick Start

First, clone the repository and navigate to the project directory:

```bash
git clone https://github.com/Maisam-dev/Task_Management.git
cd Task_Management
```

Choose one of the following methods to run the application:

## Option 1: Running with Docker (Recommended)

Run the entire application (API & SQL Server) instantly using Docker, with no need to install .NET 10 SDK or SQL Server locally.

### Prerequisites

Docker Desktop installed and running.

### Steps:

**1- Configure Environment Variables (Optional):**

Open `docker-compose.override.yml` and update the JWT key or database password if needed:

```yaml
environment:
  - ConnectionStrings__DefaultConnection=Server=sqlserver,1433;Database=Task_ManagementDb;User Id=sa;Password=YOUR_SECURE_PASSWORD!;TrustServerCertificate=True;
  - JWT__Key=YOUR_SUPER_SECRET_KEY_HERE_AT_LEAST_32_CHARS
```

**2- Start the Containers:**

```bash
docker compose up -d --build
```

**3- Apply Database Migrations:**

*(Required to create database tables inside the Docker container)*

```bash
dotnet ef database update --project Task_Management_Api
```

**4- Access Swagger UI:**

Open your browser at: http://localhost:5000/swagger

---

## Option 2: Running Locally (Without Docker)

Run the application directly on your local machine using .NET 10 SDK and a local SQL Server instance.

### Prerequisites

* .NET 10 SDK installed.
* Local SQL Server instance running.

### Steps:

**Configure Database & JWT:**

**1- Open `Task_Management_Api/appsettings.json` and set your connection string and JWT secret key:**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=Task_ManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },

  "JWT": {
    "Key": "YOUR_SUPER_SECRET_KEY_HERE_AT_LEAST_32_CHARS"
  }
}
```

**Apply Database Migrations:**

```bash
dotnet ef database update --project Task_Management_Api
```

**Run the Application:**

```bash
dotnet run --project Task_Management_Api
```

**Access Swagger UI:**

Open your browser at: https://localhost:7051/swagger (or http://localhost:5000/swagger)

---

## Demo Credentials (Auto-Seeded Data)

The API includes programmatic Data Seeding (OnModelCreating) that automatically populates the database with test users, companies, and tasks upon running the database update.

**User1:** `admin@mail.com` / `password1` — **Role1:** Admin

**User2:** `alex@mail.com` / `alex123` — **Role2:** User

**User3:** `ana@mail.com` / `ana123` — **Role3:** User

---

## Architecture & Roadmap

### Security:

Built with ASP.NET Core Identity & JWT. Designed to scale seamlessly toward dedicated Identity Providers (e.g., OpenIddict) if multi-app SSO is required.

### Roadmap:

Planned support for Refresh Tokens and Redis-based token revocation (Logout).

### Scalability:

If the project grows in complexity, adopting CQRS (Command Query Responsibility Segregation) is recommended to separate read and write operations. This can improve maintainability, scalability, and make future development easier.
