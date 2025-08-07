# 🎓 School Management System - Clean Architecture (.NET 8)

This is a modular and scalable School Management System built with **ASP.NET Core 8**, following **Clean Architecture principles**. It includes multiple layers (API, Service, Infrastructure, and Data) to separate concerns and improve maintainability.

---

## 📂 Project Structure

```
SchoolManagmentCleanArchitecture/
│
├── SchoolManagment.Api/             # Presentation Layer (ASP.NET Core Web API)
├── SchoolManagment.Core/            # Core Layer (CQRS, MediatR, Validation, Localization)
├── SchoolManagment.Service/         # Application Logic Layer (Services, Business Rules)
├── SchoolManagment.Infrastructure/  # Infrastructure Layer (EF Core, Email)
├── SchoolManagment.Data/            # Data Layer (Entities, DTOs, Enums, Seeders)
```


---

## 🚀 Features

- 🧠 **Clean Architecture** with clear separation between layers
- 🧾 **Logging** using Serilog with **Console** and **SQL Server Sink**
- 📧 **Email Sending** using MailKit (SMTP)
- 📂 **File Upload** support (stored in `wwwroot`)
- 🔐 **JWT Authentication** and **Role-Based Authorization**
- 🔁 **CQRS** (Command Query Responsibility Segregation)
- 📬 **MediatR** for decoupled request/response handling
- 🧩 **Entity Framework Core 8** with SQL Server
- 🌐 **Global Exception Handling Middleware**
- 🌍 **Localization** with resource files

---

## 🔧 Technologies Used

- **.NET 8**
- **Entity Framework Core 8**
- **MailKit** for email handling
- **Serilog** for structured logging
- **SQL Server**
- **ASP.NET Core Web API**
- **MediatR** for CQRS
- **Clean Architecture + Dependency Injection**

---

## 🛠️ How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/AbdallahElshenawy/SchoolManagmentCleanArchitecture.git
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Update the database (if needed):
   ```bash
   dotnet ef database update --project SchoolManagment.Infrastructure
   ```

4. Run the API:
   ```bash
   cd SchoolManagment.Api
   dotnet run
   ```


