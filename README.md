# Almny — E-Learning Platform API

Almny is a backend REST API for an e-learning platform built with **ASP.NET Core 8** and **Entity Framework Core**. It supports course creation, student enrollment, lesson progress tracking, exams, and AI-powered quiz generation via OpenAI.

---

## Architecture

The solution follows **Clean Architecture** and is organized into four projects:

| Project | Responsibility |
|---|---|
| `Almny.API` | Controllers, routing, middleware, app startup |
| `Almny.Application` | DTOs and service interfaces |
| `Almny.Domain` | Domain entities (pure C# classes, no dependencies) |
| `Almny.Infrastructure` | External service implementations (JWT, OpenAI) |
| `Almny.Persistence` | EF Core `DbContext`, migrations, SQL Server |

---

## Features

- **Authentication** — JWT-based register/login with BCrypt password hashing
- **Role-based authorization** — `Student`, `Instructor`, and `Admin` roles
- **Courses** — Instructors can create courses with sections and lessons
- **Enrollments** — Students can enroll in courses and view their enrolled courses
- **Lesson Progress** — Students can mark lessons as complete and track their progress
- **Exams** — Instructors can create exams with multiple-choice questions; students can submit answers and receive a score
- **AI Quiz Generation** — Instructors can send lesson content and receive AI-generated MCQ questions powered by OpenAI GPT-4o Mini
- **Swagger UI** — Interactive API docs available in development

---

## Tech Stack

- .NET 8 / ASP.NET Core
- Entity Framework Core + SQL Server
- JWT Bearer Authentication
- BCrypt.Net for password hashing
- OpenAI .NET SDK (GPT-4o Mini)
- Swagger / Swashbuckle

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (local instance or connection string to a remote server)
- An OpenAI API key

### 1. Clone the repository

```bash
git clone https://github.com/your-username/Almny.git
cd Almny
```

### 2. Configure settings

Edit `Almny.API/appsettings.json` (or use user secrets / environment variables):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=AlmnyDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "YOUR_SUPER_SECRET_KEY_HERE",
    "Issuer": "AlmnyAPI",
    "Audience": "AlmnyClient"
  },
  "OpenAI": {
    "ApiKey": "YOUR_OPENAI_API_KEY"
  }
}
```

> ⚠️ **Never commit real API keys or secrets to source control.** Use [.NET user secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) or environment variables for sensitive values.

### 3. Apply database migrations

```bash
dotnet ef database update --project Almny.Persistence --startup-project Almny.API
```

### 4. Run the API

```bash
dotnet run --project Almny.API
```

Swagger UI will be available at `https://localhost:{port}/swagger` in development.

---

## API Endpoints

### Auth — `api/auth`

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| POST | `/register` | Public | Register a new user |
| POST | `/login` | Public | Login and receive a JWT token |

### Courses — `api/courses`

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| POST | `/` | Instructor / Admin | Create a new course |
| GET | `/` | Public | List all courses |

### Sections — `api/sections`

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| POST | `/` | Instructor / Admin | Add a section to a course |

### Lessons — `api/lessons`

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| POST | `/` | Instructor / Admin | Add a lesson to a section |

### Enrollments — `api/enrollments`

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| POST | `/` | Authenticated | Enroll in a course |
| GET | `/my-courses` | Authenticated | List my enrolled courses |

### Progress — `api/progress`

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| POST | `/complete` | Authenticated | Mark a lesson as completed |
| GET | `/my-progress` | Authenticated | Get my lesson progress |

### Exams — `api/exams`

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| POST | `/` | Instructor / Admin | Create an exam for a course |
| POST | `/question` | Instructor / Admin | Add a question to an exam |
| POST | `/submit` | Authenticated | Submit exam answers and get a score |

### AI — `api/ai`

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| POST | `/generate-quiz` | Instructor / Admin | Generate 5 MCQ questions from lesson content using AI |

---

## Domain Model

```
User
 ├── Courses (as Instructor)
 ├── Enrollments (as Student)
 ├── LessonProgresses
 └── ExamAttempts

Course
 ├── Sections
 │    └── Lessons
 ├── Enrollments
 └── Exams
       └── Questions
```

---

## Project Structure

```
Almny/
├── Almny.API/
│   ├── Controllers/
│   │   ├── AIController.cs
│   │   ├── AuthController.cs
│   │   ├── CoursesController.cs
│   │   ├── EnrollmentsController.cs
│   │   ├── ExamsController.cs
│   │   ├── LessonsController.cs
│   │   ├── ProgressController.cs
│   │   └── SectionsController.cs
│   └── Program.cs
├── Almny.Application/
│   ├── DTOs/
│   └── Interfaces/
├── Almny.Domain/
│   └── Entities/
├── Almny.Infrastructure/
│   └── Services/
│       ├── AIQuizService.cs
│       └── JwtService.cs
└── Almny.Persistence/
    ├── Context/
    └── Migrations/
```
