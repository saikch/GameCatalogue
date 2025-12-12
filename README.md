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

GameCatalogue/
│
├── GamesCatalogue.Api/
│   ├── Controllers/
│   │   └── VideoGamesController.cs
│   ├── Properties/
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── Program.cs
│   └── GamesCatalogue.Api.csproj
│
├── GamesCatalogue.Application/
│   ├── DTOs/                 # Data Transfer Objects
│   ├── Entities/             # Domain entities
│   ├── Interfaces/           # Service contracts
│   ├── Services/             # Application business logic
│   └── GamesCatalogue.Application.csproj
│
├── GamesCatalogue.Infrastructure/
│   ├── Data/                 # EF Core DbContext
│   ├── Migrations/           # EF Core migrations
│   ├── Repositories/         # Data access implementations
│   └── GamesCatalogue.Infrastructure.csproj
│
├── GamesCatalogue.Web.UI/
│   ├── src/
│   │   ├── app/
│   │   │   ├── core/
│   │   │   └── features/
│   │   │       └── games/
│   │   │           ├── models/
│   │   │           │   └── game.model.ts
│   │   │           ├── pages/
│   │   │           │   ├── games-list/
│   │   │           │   └── games-edit/
│   │   │           └── services/
│   │   │               └── games.service.ts
│   │   └── environments/
│   ├── angular.json
│   └── package.json
│
├── GamesCatalogue.slnx
├── .gitignore
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

