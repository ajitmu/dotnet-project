# Practical 10 - Student Management REST API (ASP.NET Core)

A simple REST API for managing students, using an in-memory list as the data store.

## Run

```bash
dotnet run
```

The API listens on `http://localhost:5289`. Use `Practical-10-Student-REST-API.http` (VS / VS Code REST Client) to test the endpoints.

## Endpoints

| Method | URL                  | Description          |
|--------|----------------------|----------------------|
| GET    | /api/students        | Get all students     |
| GET    | /api/students/{id}   | Get student by ID    |
| POST   | /api/students        | Add a new student    |
| PUT    | /api/students/{id}   | Update a student     |
| DELETE | /api/students/{id}   | Delete a student     |

## Validation

- `Name`, `Email` are required; `Email` must be a valid email address
- `Semester` must be between 1 and 8
