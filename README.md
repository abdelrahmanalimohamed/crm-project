# Small CRM System

A modular and scalable Customer Relationship Management (CRM) system built with **C#**, **Domain-Driven Design (DDD)**, **Clean Architecture**, and a **React + TypeScript** frontend.

## 🧱 Architecture Overview

This project follows **Clean Architecture** principles to ensure separation of concerns, testability, and maintainability. The backend is structured using **DDD** to organize the business logic effectively.

### Technologies Used

#### Backend

* C# (.NET 7+)
* ASP.NET Core Web API
* Entity Framework Core
* MediatR (CQRS)
* AutoMapper
* FluentValidation
* Unit of Work / Repository Pattern
* DDD Aggregates, Entities, Value Objects

#### Frontend

* React 18+
* TypeScript
* React Router
* Axios (for HTTP requests)
* TailwindCSS or Material UI (optional UI layer)
* React Query or Zustand (for state management)

## 📂 Project Structure

### Backend (C# .NET - Clean Architecture)

```
/src
  /CRM.Application       --> Application layer (CQRS, Interfaces, DTOs)
  /CRM.Domain            --> Domain layer (Entities, Aggregates, Value Objects)
  /CRM.Infrastructure    --> Infrastructure (EF Core, DB Context, Repositories)
  /CRM.WebAPI            --> Presentation layer (Controllers, Swagger, DI)
```

### Frontend (React + TypeScript)

```
/client
  /src
    /components          --> Reusable UI components
    /features            --> Feature-based modules (e.g., customers, leads)
    /services            --> API calls via Axios
    /hooks               --> Custom React hooks
    /store               --> State management
    /pages               --> Route-based pages
    /types               --> TypeScript types and interfaces
```

## 🔧 Getting Started

### Prerequisites

* [.NET 7 SDK](https://dotnet.microsoft.com/download)
* [Node.js 18+](https://nodejs.org/)
* [SQL Server](https://www.microsoft.com/en-us/sql-server)

### Backend Setup

```bash
cd src/CRM.WebAPI
dotnet restore
dotnet ef database update
dotnet run
```

API should be running at `https://localhost:5001`.

### Frontend Setup

```bash
cd client
npm install
npm run dev
```

Frontend should be running at `http://localhost:3000`.

## 🧪 Testing

* Backend: xUnit / NUnit
* Frontend: Jest + React Testing Library

## ✅ Features

* 👤 Customer management (Create, Update, Delete)
* 📋 Lead tracking
* 📞 Contact logs
* 🔍 Search and filtering
* 📊 Dashboard overview (optional)
* 🧩 Modular structure using feature folders
* 🔐 JWT Authentication (optional)

## 📌 Domain Concepts

Example Aggregate:

```csharp
public class Customer : AggregateRoot
{
    public Guid Id { get; private set; }
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public List<Note> Notes { get; private set; }

    public void AddNote(Note note) {
        Notes.Add(note);
    }
}
```

## 📚 Resources

* [Clean Architecture by Uncle Bob](https://8thlight.com/blog/uncle-bob/2012/08/13/the-clean-architecture.html)
* [Microsoft DDD Guide](https://docs.microsoft.com/en-us/azure/architecture/microservices/model/domain-analysis)
* [React TypeScript Cheatsheet](https://react-typescript-cheatsheet.netlify.app/)

## 📦 Future Enhancements

* ✅ Authentication & Authorization (OAuth2/JWT)
* 🔄 Real-time updates (SignalR)
* ☁️ Cloud Deployment (Docker + Azure/AWS)
* 📱 Responsive UI for mobile support

## 🤝 Contributing

1. Fork the repo
2. Create a feature branch
3. Commit your changes
4. Open a PR
