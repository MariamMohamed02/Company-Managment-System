# Company Management System

A  web-based administrative platform built using ASP.NET MVC and Entity Framework Core. This system is tailored for administrative use only, providing centralized control over employees, departments, and system roles.

The project is designed using a **clean, scalable architecture (3-Tier)** with built-in support for **authentication,  authorization, secure token handling, and password recovery via email**.

---

## ✨ Key Features

### 🔒 Authentication & Authorization
- **Token-based authentication** using ASP.NET Identity.
- Built-in `UserManager` and `RoleManager` used for managing users and their roles.


### 👥 User & Role Management
- View all users in the system and manage their roles.
- Assign or update user roles via the admin interface.

### 🏢 Department Management
- Add, edit, delete, and list departments.
- Department configuration handled via EF Fluent API.

### 👨‍💼 Employee Management
- Full CRUD functionality for employee records.
- Employees can be assigned to departments and categorized by roles.

### 📧 Password Reset via Email
- Users can request a password reset by providing their registered email.
- Email with secure token-based reset link is sent automatically.
- Secure and customizable reset form provided.

### 📦 Use of DTO's (Data Transfer Objects)
- Uses DTOs in the presentation layer for clean data transfer between UI and business logic/database.


---

## 🧱 Project Architecture – 3 Tier

The application uses a **3-Tier Architecture** to maintain separation of concerns:

### 1. Presentation Layer (`Company.Project.PresentationLayer`)
Contains:
- **Controllers** – handle incoming HTTP requests and coordinate with the BLL.
- **Views** – Razor Views that build the UI for the admin panel.
- **DTOs** – transfer models between views and business logic cleanly.

### 2. Business Logic Layer (`Company.Project.BusinessLayer`)
Contains:
-  Interfaces for repositories (`IDepartmentRepository`, `IEmployeeRepository`, `IGenericRepository`, `IUnitOfWork`)
-  Repository Implementations – managing logic before interacting with the database.
-  `UnitOfWork` – coordinates multiple repository operations as one atomic transaction.

### 3. Data Access Layer (`Company.Project.DataLayer`)
Structured as:
- **Models**: `AppUser`, `Employee`, `Department`, `BaseEntity` (common props like `Id`, `CreatedDate`, etc.)
- **Configurations**: Entity Framework Fluent API configuration classes like `DepartmentConfigurations.cs`, `EmployeeConfigurations.cs`.
- **Contexts**: `CompanyDbContext.cs` sets up the EF Core database context.
- **Migrations**: Manages DB schema changes via EF Core.

---

## 🎯 Design Patterns & Principles

- **Repository Pattern** – clean abstraction over data access logic.
- **Unit of Work Pattern** – wraps multiple repository calls into one transactional unit.
- **Generic Repository** – reusable data operations for any entity.
- **AutoMapper** – automatic mapping between domain models and DTOs.
- **DTO Pattern** – prevents direct exposure of domain entities to views.

---

## 🛠️ Technologies Used

| Category             | Technology                      |
|----------------------|----------------------------------|
| Backend Framework     | ASP.NET MVC (.NET 8)            |
| Language             | C#                              |
| ORM                  | Entity Framework Core           |
| Authentication       | ASP.NET Identity + Token-Based  |
| Role Management      | RoleManager, UserManager        |
| Email Support        | SMTP (for password reset)       |
| Mapping              | AutoMapper                      |
| Frontend             | Razor Views , Bootstrap                    |
| Architecture         | 3-Tier                          |
| Data Validation      | Data Annotations, Fluent API    |
                  

---



