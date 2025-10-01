# E-Learning Platform

A web-based E-Learning system built with ASP.NET Core and Entity Framework Core.

---

## Table of Contents

- [Project Structure](#project-structure)
- [Features](#features)
- [Main Models](#main-models)
- [DTOs](#dtos)
- [Controllers](#controllers)
- [Repositories & Interfaces](#repositories--interfaces)
- [Services](#services)
- [Extensions](#extensions)
- [Middleware](#middleware)
- [Migrations](#migrations)
- [Getting Started](#getting-started)

---

## Project Structure

```
E-Learning/
│
├── Controllers/         # API Controllers (Account, Course, Department, Enrollment, Lecture, User)
├── Data/                # EF Core DbContext, repositories, and models
├── DTOs/                # Data Transfer Objects for API requests/responses
├── Extenstions/         # Extension methods and DI setup
├── Helpers/             # AutoMapper profiles and helpers
├── Interfaces/          # Repository and service interfaces
├── Middleware/          # Custom middleware (e.g., exception handling)
├── Migrations/          # EF Core migrations
├── services/            # Business logic and utility services
├── settings/            # Configuration classes
├── wwwroot/             # Static files (e.g., uploaded lectures)
├── Program.cs           # Application entry point
├── appsettings.json     # Configuration
└── README.md            # Project documentation
```

---

## Features

- User authentication (JWT)
- Role-based authorization (Admin, Instructor, User)
- Course, Department, Enrollment, and Lecture management (CRUD)
- PDF lecture upload and download
- Exception handling middleware
- AutoMapper for DTO mapping
- RESTful API endpoints
- Swagger API documentation

---

## Main Models

- **User**:  
  SSN, FirstName, LastName, Email, Password, Role, Dept_id, Department, Enrollments

- **Course**:  
  Id, Course_Name, Course_Description, Dept_Id_FK, Department, Enrollments, Lectures

- **Department**:  
  Dept_Id, Name, Courses, Users

- **Lecture**:  
  Id, Lec_Name, Lec_source (PDF), Course_ID, Course

- **Enrollment**:  
  Course_ID, User_ID, EnrollDate, User, Course

---

## DTOs

- RegisterDTO: User registration
- LoginDTO: User login
- NewUserDTO: Login response with JWT
- CourseDTO, CourseResponse, GetAllCourses: Course operations
- DepartmentDTO, DepartmentResponse: Department operations
- LectureDTO, LectureResponse: Lecture operations
- EnrollmentResponse: Enrollment responses

---

## Controllers

- **AccountController**: Registration, login, JWT issuance
- **UserController**: User profile, admin user management
- **CourseController**: CRUD for courses
- **DepartmentController**: CRUD for departments
- **EnrollmentController**: Enroll/unenroll users in courses
- **LectureController**: Manage lectures and file uploads

---

## Repositories & Interfaces

- ICourseRepository, CourseRepository
- IDepartmentRepository, DepartmentRepository
- IEnrollmentRepository, EnrollmentRepository
- ILectureRepository, LectureRepository
- IFileServices, FileServices
- ITokenServices, TokenServices

---

## Services

- **TokenServices**: JWT token creation
- **FileServices**: Handles lecture PDF uploads

---

## Extensions

- **IdentityExtension**: JWT authentication setup
- **ApplicationServices**: Dependency injection for repositories and services
- **UserIdExtenstion**: Extracts user ID from JWT claims

---

## Middleware

- **ExceptionHandler**: Global exception handling, returns JSON error responses
- **ErrorDetials**: Error response model

---

## Migrations

- EF Core migration files for database schema

---

## Getting Started

1. Clone the repository.
2. Configure your database connection in `appsettings.json`.
3. Run migrations:
   ```sh
   dotnet ef database update
   ```
4. Run the project:
   ```sh
   dotnet run
   ```
5. Access Swagger UI at `/swagger` for API documentation.

