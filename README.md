# Course Enrollment Management API

A **Course Enrollment Management API** built with **.NET 9, Entity Framework Core, and Identity**.  
This project provides a backend service to manage **courses, students, and enrollments**, including authentication and authorization with **JWT tokens**.

---

## 📌 Features

- User authentication and role-based authorization (JWT + Identity)
- Course CRUD operations (create, update, delete, list)
- Student enrollment management
- Teacher and Student management
- Swagger UI with JWT authentication support
- SQL Server database with Entity Framework Core

---

## 🛠️ Technologies

- **.NET 9** (ASP.NET Core Web API)  
- **Entity Framework Core**  
- **ASP.NET Identity + JWT Authentication**  
- **SQL Server**  
- **Swagger / OpenAPI**

---

## 🚀 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)  
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)  
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)  

---

### Installation

1. Clone the repository:

```
git clone https://github.com/yourusername/course-enrollment-api.git
cd course-enrollment-api
```

2. Configure the database connection in `appsettings.json`:

```
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=CourseDb;Trusted_Connection=True;"
},
"JWT": {
  "Issuer": "YourApp",
  "Audience": "YourAppUsers",
  "SigningKey": "YourSuperSecretKey123!"
}
```

3. Apply migrations and create the database:

```
dotnet ef database update
```

4. Run the API:

```
dotnet run
```

The API will be available at `https://localhost:5100`.

---

## 📄 API Endpoints

### Account

- `POST /api/account/signup/student` - Register a student  
- `POST /api/account/signup/teacher` - Register a teacher  
- `POST /api/account/login` - Login and receive JWT token  
- `GET /api/account/users` - List all users  

### Courses

- `GET /api/course` - List all courses  
- `POST /api/course` - Create a course  
- `GET /api/course/{id}` - Get course by ID  
- `PUT /api/course/{id}` - Update a course  
- `DELETE /api/course/{id}` - Delete a course  

### Enrollments

- `GET /api/enroll` - List all enrollments  
- `POST /api/enroll` - Create enrollment  
- `GET /api/enroll/{id}` - Get enrollment by ID  

### Students & Teachers

- `GET /api/student` - List students  
- `GET /api/student/{id}` - Get student by ID  
- `PUT /api/student/{id}` - Update student  
- `DELETE /api/student/{id}` - Delete student  

- `GET /api/teacher` - List teachers  
- `GET /api/teacher/{id}` - Get teacher by ID  
- `PUT /api/teacher/{id}` - Update teacher  
- `DELETE /api/teacher/{id}` - Delete teacher  

---

## 🔑 JWT Authentication

1. Use the **login endpoint** to receive a JWT token.
2. Click "Authorize" in Swagger UI and paste the token as:

```
Bearer YOUR_JWT_TOKEN
```

3. Protected endpoints will now be accessible according to user role.

---

## 💡 Project Structure

```
api/
├─ Controllers/       # API endpoints
├─ Dtos/              # Data transfer objects
├─ Models/            # Entity models
├─ Repository/        # Database access logic
├─ Services/          # Token service & business logic
├─ Data/              # EF Core DbContext
```

---

## 🏗️ Future Improvements

- Add **unit tests** and **integration tests**  
- Implement **refresh tokens** for JWT  
- Add **pagination & filtering** for lists  
- Improve **Swagger documentation** with examples  

---

## 📄 License

This project is **open source** and available under the [MIT License](LICENSE).

---
