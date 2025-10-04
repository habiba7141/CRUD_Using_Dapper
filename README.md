# 🚀 CRUD Operations with Dapper & ASP.NET Core

This project is a **CRUD Web API** built using **ASP.NET Core** and **Dapper** as a micro-ORM for high-performance data access.  
It demonstrates how to manage **Employee records** with Create, Read, Update, and Delete operations.

---

## ✨ Features
- 🔹 **Create**: Add new employees to the database.  
- 🔹 **Read**: Fetch all employees or get a specific one by code.  
- 🔹 **Update**: Modify employee details by code.  
- 🔹 **Delete**: Remove employees from the system.  
- 🔹 Built with **Dapper** for lightweight and efficient database queries.  
- 🔹 RESTful API with **Swagger** for documentation.  
- 🔹 Supports **Stored Procedures** for cleaner, faster, and reusable SQL logic.  
- 🔹 Implements **Transactions** to ensure data integrity during multiple operations.  
- 🔹 Handles **Multiple Queries** efficiently within a single database call for optimized performance.  

---

## 🛠️ Tech Stack
- **ASP.NET Core 8** → Web API  
- **Dapper** → Micro ORM  
- **SQL Server** → Database  
- **Swagger / Swashbuckle** → API testing & documentation  

---

## ⚙️ Advanced Dapper Features

### 🧩 Stored Procedures
The project uses **stored procedures** to encapsulate SQL logic and improve performance & security.  

**Example:**
```sql
CREATE PROCEDURE GetEmployeeByCode
    @Code INT
AS
BEGIN
    SELECT * FROM Employees WHERE Code = @Code;
END
