# Inventory Management System

This project is a web-based inventory management system built using **ASP.NET Core 8**. It allows users to manage products, categories, suppliers, inventory levels, and stock transactions. Each user has their own account, and all data is isolated so users can only access their own information.

The project also includes a REST API for managing team members.

---

## Technologies Used

- ASP.NET Core 8 (Razor Pages and REST API)
- SQL Server
- Dapper with Stored Procedures
- ASP.NET Core Identity
- xUnit
- Moq

---

## Requirements

Before running the project locally, install:

- .NET 8 SDK
- SQL Server (LocalDB or a local SQL Server instance)

---

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/garmartirosy/InventoryManagement
```

### 2. Create the database

Create a SQL Server database named:

```text
InventoryManagementDB
```

### 3. Create the ASP.NET Core Identity tables

Run:

```bash
dotnet ef database update
```

### 4. Run the application

```bash
dotnet run
```

---

## Running the Tests

Run the test project with:

```bash
dotnet test
```

The solution includes **15 unit tests** covering:

- Model validation
- REST API controller behavior
- Category caching

The tests use **Moq** to mock dependencies, so **SQL Server is not required** to run the test suite.

---

