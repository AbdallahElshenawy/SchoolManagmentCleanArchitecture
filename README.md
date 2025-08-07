# 🎓 School Management System - Clean Architecture (.NET 8)

This is a modular and scalable School Management System built with **ASP.NET Core 8**, following **Clean Architecture principles**. It includes multiple layers (API, Service, Infrastructure, and Data) to separate concerns and improve maintainability.

---

## 📂 Project Structure

```
SchoolManagmentCleanArchitecture/
│
├── SchoolManagment.Api/            # API project (Presentation Layer)
├── SchoolManagment.Service/        # Application logic and business rules
├── SchoolManagment.Infrastructure/ # Infrastructure (DB, Email, Logging)
├── SchoolManagment.Data/           # Entities, DTOs, Enums
```

---

## 🚀 Features

- 🧪 Clean Architecture (Domain, Application, Infrastructure, API)
- 🧾 Logging using Serilog with SQL Sink
- 📧 Email Confirmation via SMTP (MailKit)
- 📥 File Upload (with image saving to `wwwroot`)
- 🔐 JWT Authentication & Role-Based Access
- 🔁 CQRS (Command Query Responsibility Segregation)
- 🧩 MediatR for decoupled request/response handling
- 📦 Entity Framework Core 8 for data access
- 🛡️ Security best practices (token expiration, refresh tokens)

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


