# Getting Started with WhatsNext

Welcome to WhatsNext! This guide will help you get the project up and running quickly.

## 📋 Prerequisites Checklist

Before you begin, ensure you have the following installed:

- [ ] [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [ ] [Node.js 20.x LTS](https://nodejs.org/) (for frontend, coming soon)
- [ ] [SQL Server](https://www.microsoft.com/sql-server) or [PostgreSQL](https://www.postgresql.org/)
- [ ] [Git](https://git-scm.com/)
- [ ] An IDE: [Visual Studio 2022](https://visualstudio.microsoft.com/), [VS Code](https://code.visualstudio.com/), or [Rider](https://www.jetbrains.com/rider/)

### Verify Installation

```powershell
# Check .NET version (should be 9.0+)
dotnet --version

# Check Node version (should be 20.x+)
node --version

# Check Git
git --version
```

## 🚀 Quick Start (5 Minutes)

### 1. Clone or Navigate to Repository

```powershell
cd C:\src\WhatsNext
```

### 2. Restore Backend Dependencies

```powershell
cd backend
dotnet restore
```

### 3. Build the Solution

```powershell
dotnet build
```

You should see: **Build succeeded** (with some StyleCop warnings - that's normal and shows linters are working!)

### 4. Verify Everything Works

```powershell
dotnet test
```

## 📁 Project Structure Overview

```
WhatsNext/
├── backend/                         # ✅ COMPLETE
│   ├── src/
│   │   ├── WhatsNext.Domain/       # ✅ Entities, enums
│   │   ├── WhatsNext.Application/  # 🔄 NEXT: CQRS commands/queries
│   │   ├── WhatsNext.Infrastructure/ # 🔄 NEXT: EF Core, repositories
│   │   └── WhatsNext.API/          # 🔄 NEXT: Controllers, auth
│   ├── tests/
│   │   ├── WhatsNext.UnitTests/
│   │   └── WhatsNext.IntegrationTests/
│   ├── WhatsNext.sln
│   ├── .editorconfig
│   ├── stylecop.json
│   └── Directory.Build.props
├── frontend/                        # ⏳ COMING NEXT
├── docs/
│   ├── architecture/
│   │   └── ARCHITECTURE.md         # ✅ Complete architecture docs
│   ├── api/                        # ⏳ API docs (after implementation)
│   └── images/                     # ⏳ Screenshots
├── .gitignore                      # ✅ Configured
├── README.md                       # ✅ Complete
├── CONTRIBUTING.md                 # ✅ Complete
├── PROJECT_CHARTER.md              # ✅ Complete
├── IMPLEMENTATION_STATUS.md        # ✅ Current status
└── GETTING_STARTED.md             # ✅ This file
```

## 🏗️ What's Been Built

### ✅ Completed (Phase 1a)

1. **Project Infrastructure**
   - Solution structure with Clean Architecture
   - 6 projects (4 source + 2 test)
   - All dependencies and references configured
   - 20+ NuGet packages installed

2. **Domain Layer** (Complete!)
   - 6 core entities: User, Habit, HabitCompletion, TodoTask, TimerSession, Quote
   - 2 base classes: BaseEntity, AuditableEntity
   - 4 enums: HabitFrequency, TaskPriority, TaskStatus, SessionType
   - Proper relationships and navigation properties

3. **Code Quality Setup**
   - StyleCop Analyzers
   - SonarAnalyzer.CSharp
   - Roslynator.Analyzers
   - EditorConfig for formatting
   - Directory.Build.props for centralized config

4. **Documentation**
   - README.md - Project overview
   - CONTRIBUTING.md - Contribution guidelines
   - PROJECT_CHARTER.md - Full project plan
   - ARCHITECTURE.md - Architecture diagrams and details
   - IMPLEMENTATION_STATUS.md - Current progress
   - GETTING_STARTED.md - This guide

### 🔄 Next Steps (Phase 1b)

1. **Application Layer Implementation**
   - CQRS Commands (CreateHabitCommand, etc.)
   - CQRS Queries (GetHabitsQuery, etc.)
   - MediatR Handlers
   - FluentValidation Validators
   - AutoMapper Profiles
   - DTOs

2. **Infrastructure Layer Implementation**
   - ApplicationDbContext (EF Core)
   - Entity Configurations (Fluent API)
   - Migrations
   - Repositories
   - External Services (Weather, Email, Quote APIs)

3. **API Layer Implementation**
   - Program.cs configuration
   - Controllers (Auth, Habits, Tasks, Timer, Quotes)
   - JWT Authentication
   - Swagger/OpenAPI setup
   - Exception handling middleware
   - Serilog configuration

## 🛠️ Development Workflow

### Working with the Backend

```powershell
# Navigate to backend
cd backend

# Build the solution
dotnet build

# Run the API (when ready)
dotnet run --project src/WhatsNext.API

# Run tests
dotnet test

# Run with code coverage
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# Format code
dotnet format

# Check for formatting issues
dotnet format --verify-no-changes

# Create a migration (when ready)
dotnet ef migrations add InitialCreate --project src/WhatsNext.Infrastructure --startup-project src/WhatsNext.API

# Update database (when ready)
dotnet ef database update --project src/WhatsNext.Infrastructure --startup-project src/WhatsNext.API
```

### Code Style

The project enforces strict code quality standards:

**Automatically Checked:**
- StyleCop rules (naming, formatting, documentation)
- SonarAnalyzer (bugs, vulnerabilities, code smells)
- Roslynator (refactoring opportunities)
- EditorConfig (formatting)

**To fix most issues automatically:**
```powershell
dotnet format
```

## 📚 Key Files to Understand

### Configuration Files

| File | Purpose |
|------|---------|
| `backend/Directory.Build.props` | Centralized MSBuild configuration for all projects |
| `backend/.editorconfig` | Code formatting and style rules |
| `backend/stylecop.json` | StyleCop analyzer configuration |
| `.gitignore` | Files to exclude from version control |

### Domain Layer

| File | Description |
|------|-------------|
| `Common/BaseEntity.cs` | Base class with Id, CreatedAt, UpdatedAt, IsDeleted |
| `Common/AuditableEntity.cs` | Extends BaseEntity with CreatedBy, UpdatedBy |
| `Entities/User.cs` | User entity with authentication fields |
| `Entities/Habit.cs` | Habit tracking with streaks |
| `Entities/TodoTask.cs` | Task management entity |
| `Entities/TimerSession.cs` | Pomodoro timer sessions |

### Documentation

| File | Purpose |
|------|---------|
| `README.md` | Project overview and setup instructions |
| `CONTRIBUTING.md` | How to contribute, coding standards |
| `PROJECT_CHARTER.md` | Complete project plan and objectives |
| `docs/architecture/ARCHITECTURE.md` | Architecture diagrams and design decisions |
| `IMPLEMENTATION_STATUS.md` | Detailed progress report |

## 🎯 Current Focus: Application Layer

The next major milestone is implementing the Application layer with CQRS pattern.

### What This Means

**Commands** (Write Operations):
- `CreateHabitCommand` - Add a new habit
- `UpdateHabitCommand` - Update an existing habit
- `DeleteHabitCommand` - Delete a habit
- `CompleteHabitCommand` - Mark habit as complete for today

**Queries** (Read Operations):
- `GetHabitsQuery` - Get all habits for a user
- `GetHabitByIdQuery` - Get a specific habit
- `GetHabitStatisticsQuery` - Get habit completion stats

**Handlers**:
- Each command/query has a handler that implements the business logic
- Uses MediatR for clean separation

**Validators**:
- FluentValidation ensures all inputs are valid before processing

### Example Flow

```
User Request → API Controller → MediatR.Send(Command) → Validator → Handler → Repository → Database
                                        ↓
                                  Auto-mapping to DTO ← Response
```

## 🧪 Testing Strategy

### Test Structure

```
tests/
├── WhatsNext.UnitTests/           # Fast, isolated tests
│   ├── Domain/                    # Entity behavior
│   ├── Application/               # Command/Query handlers
│   └── Infrastructure/            # Repository logic
└── WhatsNext.IntegrationTests/    # Full stack tests
    └── API/                       # Endpoint tests
```

### Running Tests

```powershell
# All tests
dotnet test

# Unit tests only
dotnet test --filter Category=Unit

# Integration tests only
dotnet test --filter Category=Integration

# With coverage
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# Verbose output
dotnet test --verbosity normal
```

## 🐛 Troubleshooting

### Build Issues

**Problem**: "Cannot find .NET SDK"
```powershell
# Solution: Install .NET 9.0 SDK
# Download from: https://dotnet.microsoft.com/download/dotnet/9.0
```

**Problem**: "Package restore failed"
```powershell
# Solution: Clear NuGet cache and restore
dotnet nuget locals all --clear
dotnet restore
```

**Problem**: "StyleCop warnings"
```
# This is normal! The warnings show that linters are working.
# To fix formatting issues automatically:
dotnet format
```

### IDE Setup

**Visual Studio 2022**:
1. Open `backend/WhatsNext.sln`
2. Build → Build Solution (Ctrl+Shift+B)
3. Test → Run All Tests (Ctrl+R, A)

**VS Code**:
1. Install C# Dev Kit extension
2. Open `backend` folder
3. Terminal → New Terminal
4. Run `dotnet build`

**Rider**:
1. Open `backend/WhatsNext.sln`
2. Build → Build Solution (Ctrl+Shift+F9)
3. Unit Tests → Run All Tests

## 📖 Learning Resources

### Clean Architecture
- [Uncle Bob's Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Microsoft Architecture eBook](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/)

### CQRS & MediatR
- [CQRS Pattern](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)
- [MediatR GitHub](https://github.com/jbogard/MediatR)

### Entity Framework Core
- [EF Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [EF Core Migrations](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/)

### Code Quality
- [StyleCop Rules](https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/DOCUMENTATION.md)
- [C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)

## 🤝 Need Help?

1. **Check Documentation**: Start with `README.md` and `ARCHITECTURE.md`
2. **Review Code**: All domain entities have XML documentation
3. **Check TODO List**: See what's next in the project
4. **Open an Issue**: Use GitHub Issues for questions or problems

## ✅ Verification Checklist

Before proceeding to the next phase, verify:

- [ ] `.NET 9 SDK installed and working`
- [ ] `Solution builds successfully (dotnet build)`
- [ ] `Tests run (even if they pass with no real tests yet)`
- [ ] `You understand the Clean Architecture layers`
- [ ] `You've reviewed the domain entities`
- [ ] `You've read the ARCHITECTURE.md document`

## 🎉 You're Ready!

The foundation is complete and solid. The next steps are:

1. **Implement Application Layer** - CQRS commands and queries
2. **Implement Infrastructure Layer** - EF Core and repositories
3. **Implement API Layer** - Controllers and authentication
4. **Set Up Frontend** - React + TypeScript + Tailwind
5. **Add DevOps** - CI/CD and pre-commit hooks

Each phase builds on the solid foundation we've created.

**Let's build something amazing!** 🚀

---

**Questions?** Open an issue or check the documentation in the `docs/` folder.

**Last Updated**: October 7, 2025

