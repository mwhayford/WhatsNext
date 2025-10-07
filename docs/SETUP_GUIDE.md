# WhatsNext - Complete Setup Guide

This guide will walk you through setting up the WhatsNext project from scratch.

## 📋 Prerequisites

### Required Software

- **Node.js** 18+ and npm
- **.NET SDK** 9.0+
- **SQL Server LocalDB** (included with Visual Studio) or SQL Server
- **Git** 2.30+
- **Visual Studio Code** or **Visual Studio 2022**

### Verify Installation

```bash
# Check Node.js and npm
node --version  # Should be v18.0.0 or higher
npm --version   # Should be 9.0.0 or higher

# Check .NET SDK
dotnet --version  # Should be 9.0.0 or higher

# Check Git
git --version  # Should be 2.30.0 or higher
```

## 🚀 Initial Setup

### 1. Clone the Repository

```bash
git clone <repository-url>
cd WhatsNext
```

### 2. Install Root Dependencies

```bash
# Install Husky for Git hooks
npm install
```

### 3. Install Frontend Dependencies

```bash
cd frontend
npm install
cd ..
```

### 4. Configure Database

#### Option A: SQL Server LocalDB (Recommended for Development)

The default configuration uses SQL Server LocalDB. No additional setup required!

```json
// Already configured in appsettings.Development.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=WhatsNextDb_Dev;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true"
  },
  "DatabaseProvider": "SqlServer"
}
```

#### Option B: PostgreSQL

1. Install PostgreSQL
2. Create a database:
   ```sql
   CREATE DATABASE whatsnext_dev;
   ```
3. Update `appsettings.Development.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=whatsnext_dev;Username=your_user;Password=your_password"
     },
     "DatabaseProvider": "PostgreSQL"
   }
   ```

### 5. Apply Database Migrations

```bash
cd backend
dotnet ef database update --project src/WhatsNext.Infrastructure --startup-project src/WhatsNext.API
```

This will:
- Create the database
- Create all tables with proper relationships
- Seed initial data (motivational quotes)

### 6. Verify Backend Build

```bash
cd backend
dotnet build
dotnet test
```

### 7. Verify Frontend Build

```bash
cd frontend
npm run build
npm run lint
npm run type-check
```

## 🏃 Running the Application

### Development Mode (Recommended)

Open two terminal windows:

**Terminal 1 - Backend:**
```bash
npm run dev:backend
# Or directly:
cd backend
dotnet run --project src/WhatsNext.API
```

**Terminal 2 - Frontend:**
```bash
npm run dev:frontend
# Or directly:
cd frontend
npm run dev
```

The application will be available at:
- Frontend: http://localhost:5173
- Backend API: https://localhost:7001
- Swagger UI: https://localhost:7001/swagger

### Production Build

```bash
# Build everything
npm run build

# Run production build
npm run start:backend
npm run preview  # Frontend preview
```

## 🔧 Development Workflow

### Git Workflow with Pre-commit Hooks

When you commit, Husky will automatically:

1. **Frontend Checks:**
   - Run ESLint and auto-fix issues
   - Run Prettier and format code
   - Type-check TypeScript

2. **Backend Checks:**
   - Verify C# formatting with `dotnet format`
   - Ensure StyleCop rules are followed

If any checks fail, the commit will be blocked until you fix the issues.

### Manual Quality Checks

```bash
# Lint everything
npm run lint

# Format everything
npm run format

# Run all tests
npm run test
```

## 📁 Project Structure

```
WhatsNext/
├── .git/                          # Git repository
├── .husky/                        # Git hooks
│   └── pre-commit                # Pre-commit hook
├── backend/                       # C# Backend
│   ├── src/
│   │   ├── WhatsNext.API/        # Web API Layer
│   │   ├── WhatsNext.Application/ # Business Logic (CQRS)
│   │   ├── WhatsNext.Domain/     # Domain Entities
│   │   └── WhatsNext.Infrastructure/ # Data Access
│   ├── tests/                    # Test Projects
│   ├── .editorconfig            # Code formatting rules
│   ├── stylecop.json            # StyleCop configuration
│   └── Directory.Build.props    # Shared build properties
├── frontend/                     # React Frontend
│   ├── src/                     # Source code
│   ├── .eslintrc.json          # ESLint configuration
│   ├── .prettierrc             # Prettier configuration
│   ├── .lintstagedrc.json      # Lint-staged configuration
│   ├── tailwind.config.js      # Tailwind CSS config
│   └── vite.config.ts          # Vite configuration
├── docs/                        # Documentation
│   └── architecture/           # Architecture diagrams
├── .gitignore                  # Git ignore rules
├── package.json                # Root package.json (scripts)
└── README.md                   # Project overview
```

## 🎯 Development Scripts

### Root Level (Orchestration)

```bash
npm run dev:frontend       # Start frontend dev server
npm run dev:backend        # Start backend dev server
npm run build              # Build both frontend and backend
npm run test               # Run all tests
npm run lint               # Lint all code
npm run format             # Format all code
```

### Backend Scripts

```bash
cd backend

# Build and run
dotnet build
dotnet run --project src/WhatsNext.API

# Testing
dotnet test
dotnet test --collect:"XPlat Code Coverage"

# Linting/Formatting
dotnet format                          # Format code
dotnet format --verify-no-changes      # Check formatting

# Database migrations
dotnet ef migrations add <MigrationName> --project src/WhatsNext.Infrastructure --startup-project src/WhatsNext.API
dotnet ef database update --project src/WhatsNext.Infrastructure --startup-project src/WhatsNext.API
dotnet ef migrations remove --project src/WhatsNext.Infrastructure --startup-project src/WhatsNext.API
```

### Frontend Scripts

```bash
cd frontend

# Development
npm run dev              # Start dev server
npm run build            # Build for production
npm run preview          # Preview production build

# Code Quality
npm run lint             # Run ESLint
npm run lint:fix         # Fix ESLint issues
npm run format           # Format with Prettier
npm run format:check     # Check Prettier formatting
npm run type-check       # TypeScript type checking
npm run validate         # Run all checks
npm run lint-staged      # Run lint-staged (used by pre-commit)
```

## 🗄️ Database Management

### Migrations

```bash
cd backend

# Create a new migration
dotnet ef migrations add <MigrationName> --project src/WhatsNext.Infrastructure --startup-project src/WhatsNext.API --output-dir Persistence/Migrations

# Apply migrations
dotnet ef database update --project src/WhatsNext.Infrastructure --startup-project src/WhatsNext.API

# Revert last migration
dotnet ef migrations remove --project src/WhatsNext.Infrastructure --startup-project src/WhatsNext.API

# View migration history
dotnet ef migrations list --project src/WhatsNext.Infrastructure --startup-project src/WhatsNext.API
```

### Database Seeding

Initial data is seeded automatically when the database is created:
- Motivational quotes (100+ quotes)

To re-seed data, drop the database and run `dotnet ef database update` again.

## 🧪 Testing

### Backend Testing

```bash
cd backend

# Run all tests
dotnet test

# Run with code coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test project
dotnet test tests/WhatsNext.UnitTests
dotnet test tests/WhatsNext.IntegrationTests
```

### Frontend Testing (To be implemented)

```bash
cd frontend

# Run tests
npm run test

# Run tests with coverage
npm run test:coverage

# Run tests in watch mode
npm run test:watch
```

## 🔍 Troubleshooting

### Backend Issues

**Issue: Database connection fails**
```bash
# Check if LocalDB is running
sqllocaldb info
sqllocaldb start mssqllocaldb

# If LocalDB is not installed, switch to PostgreSQL or SQL Server
```

**Issue: Build errors after pulling latest code**
```bash
cd backend
dotnet clean
dotnet restore
dotnet build
```

**Issue: Migration fails**
```bash
# Drop the database and recreate
dotnet ef database drop --project src/WhatsNext.Infrastructure --startup-project src/WhatsNext.API
dotnet ef database update --project src/WhatsNext.Infrastructure --startup-project src/WhatsNext.API
```

### Frontend Issues

**Issue: Module not found errors**
```bash
cd frontend
rm -rf node_modules package-lock.json
npm install
```

**Issue: Vite dev server fails to start**
```bash
# Check if port 5173 is in use
# Windows:
netstat -ano | findstr :5173
# Kill the process using the port if needed

# Or change the port in vite.config.ts
```

**Issue: TypeScript errors**
```bash
cd frontend
npm run type-check
# Fix any type errors before running dev server
```

### Git Hook Issues

**Issue: Pre-commit hook fails**
```bash
# Run checks manually to see detailed errors
npm run lint
npm run format

# If hooks are causing issues, you can temporarily bypass them
git commit --no-verify -m "Your message"
# But please fix the issues afterward!
```

**Issue: Husky not installed**
```bash
# Reinstall Husky
npm install
npx husky install
```

## 🌐 Environment Variables

### Backend (appsettings.json)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your connection string"
  },
  "DatabaseProvider": "SqlServer", // or "PostgreSQL"
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

### Frontend (.env files - to be created)

```bash
# .env.development
VITE_API_BASE_URL=https://localhost:7001/api

# .env.production
VITE_API_BASE_URL=https://api.yourproduction.com/api
```

## 📚 Additional Resources

- [C4 Architecture Diagrams](./architecture/ARCHITECTURE.md)
- [Contributing Guidelines](../CONTRIBUTING.md)
- [Project Charter](../PROJECT_CHARTER.md)
- [Implementation Status](../IMPLEMENTATION_STATUS.md)
- [Frontend README](../frontend/README.md)

## ✅ Setup Verification Checklist

- [ ] Node.js 18+ installed
- [ ] .NET SDK 9.0+ installed
- [ ] Git installed
- [ ] Repository cloned
- [ ] Root dependencies installed (`npm install`)
- [ ] Frontend dependencies installed (`cd frontend && npm install`)
- [ ] Database configured (LocalDB or PostgreSQL)
- [ ] Migrations applied (`dotnet ef database update`)
- [ ] Backend builds successfully (`cd backend && dotnet build`)
- [ ] Backend tests pass (`cd backend && dotnet test`)
- [ ] Frontend builds successfully (`cd frontend && npm run build`)
- [ ] Frontend linting passes (`cd frontend && npm run lint`)
- [ ] Frontend type-checking passes (`cd frontend && npm run type-check`)
- [ ] Backend runs (`npm run dev:backend`)
- [ ] Frontend runs (`npm run dev:frontend`)
- [ ] Can access https://localhost:7001/swagger
- [ ] Can access http://localhost:5173
- [ ] Pre-commit hooks work (try making a commit)

## 🎉 You're All Set!

Your development environment is now fully configured. Start coding!

For questions or issues, please refer to the [CONTRIBUTING.md](../CONTRIBUTING.md) or open an issue on GitHub.

