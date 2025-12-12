# Game Catalogue (Video Games) — Technical Assessment

A simple two-page Video Games catalogue:
1) Browse games (list)
2) Edit a game (create/update)

The focus of this solution is clean structure, readability, and an end-to-end working flow (API + UI).

## Tech Stack

### Backend
- .NET 10 / ASP.NET Core Web API
- Entity Framework Core (Code First)
- SQL Server (LocalDB / SQL Express)

### Frontend
- Angular 21
- Angular Router
- Bootstrap / ng-bootstrap (light UI styling)

## Repository Structure

GamesCatalogue/
│
├── GamesCatalogue.Api/
│   ├── Controllers/
│   │   └── VideoGamesController.cs
│   ├── Properties/
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   └── Program.cs
│
├── GamesCatalogue.Application/
│   ├── DTOs/
│   │   ├── VideoGameDto.cs
│   │   ├── CreateVideoGameDto.cs
│   │   └── UpdateVideoGameDto.cs
│   │
│   ├── Entities/
│   │   └── VideoGame.cs
│   │
│   ├── Interfaces/
│   │   ├── IVideoGameService.cs
│   │   └── IVideoGameRepository.cs
│   │
│   └── Services/
│       └── VideoGameService.cs
│
├── GamesCatalogue.Infrastructure/
│   ├── Data/
│   │   └── ApplicationDbContext.cs
│   │
│   ├── Migrations/
│   │
│   └── Repositories/
│       └── VideoGameRepository.cs
│
├── GamesCatalogue.Web.UI/
│   └── src/
│       └── app/
│           └── features/
│               └── games/
│                   ├── models/
│                   ├── pages/
│                   └── services/
│
├── GamesCatalogue.Tests/
│   ├── Services/
│   │   └── VideoGameServiceTests.cs
│   │
│   └── Controllers/
│       └── VideoGamesControllerTests.cs
│
├── GamesCatalogue.slnx
└── README.md



## Prerequisites

- .NET 10 SDK
- Node.js 20.x
- SQL Server (LocalDB or SQL Express recommended)

## How to Run

### 1) Backend (API)

1. Configure the database connection string:
   - Update `GamesCatalogue.Api/appsettings.Development.json` (or `appsettings.json`) to point to your SQL Server instance.

2. Apply EF Core migrations:
   - Using Package Manager Console (Visual Studio):
     - `Update-Database -StartupProject GamesCatalogue.Api`
   - Or using CLI (from solution root):
     - `dotnet ef database update --project GamesCatalogue.Infrastructure --startup-project GamesCatalogue.Api`

3. Run the API:
   - From Visual Studio: set `GamesCatalogue.Api` as the startup project → Run
   - Or via CLI:
     - `dotnet run --project GamesCatalogue.Api`

4. Swagger (API documentation):
   - When running in Development, Swagger UI is available at:
     
    
   - `http://localhost:5205/swagger`
    

The API base URL and ports will be shown in the console output.


### 2) Frontend (Angular)

1. Go to the Angular project:
   - `cd GamesCatalogue.Web.UI`

2. Install dependencies:
   - `npm install`

3. Run:
   - `npm start` or
   - `ng serve`

Open:
- `http://localhost:4200`


### Testing

The solution includes unit tests for the service and controller layers to ensure correct behavior and API responses.

1.Service Tests
2.Controller Tests

Running Tests

### To run all tests:

dotnet test

From Visual Studio:

- Open Test Explorer

- Click Run All

- Confirm all Service and Controller tests pass successfully.



