# WhatsNext - Personal Productivity Dashboard

[![Backend CI/CD](https://github.com/mwhayford/WhatsNext/workflows/Backend%20CI%2FCD/badge.svg)](https://github.com/mwhayford/WhatsNext/actions)
[![Frontend CI/CD](https://github.com/mwhayford/WhatsNext/workflows/Frontend%20CI%2FCD/badge.svg)](https://github.com/mwhayford/WhatsNext/actions)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A production-grade, portfolio-worthy personal dashboard application built with C# Web API backend and React.js frontend. This project demonstrates enterprise-level code quality, DevOps best practices, and modern full-stack development.

![Dashboard Screenshot](docs/images/dashboard-preview.png)

## 🚀 Features

### Currently Implemented
- ✅ **User Authentication** - JWT-based secure authentication (Register & Login)
- ✅ **Database Models** - Full schema for habits, tasks, timer sessions, and quotes
- ✅ **Docker Support** - Complete containerization with PostgreSQL
- ✅ **CI/CD Pipeline** - Automated testing and deployment
- ✅ **API Documentation** - Swagger/OpenAPI integration
- ✅ **Clean Architecture** - CQRS pattern with MediatR
- ✅ **Developer Tooling** - Linters, pre-commit hooks, comprehensive docs

### In Progress (Backend Ready)
- 🔨 **Habit Tracker** - Track daily habits with streak visualization
- 🔨 **Task Manager** - Create, organize, and complete tasks
- 🔨 **Pomodoro Timer** - Productivity timer with session tracking
- 🔨 **Quote Generator** - Daily motivational quotes

### Coming Soon
- 📝 Weather Widget integration
- 📝 Note Taking with rich text editor
- 📊 Analytics Dashboard with productivity insights
- 🎯 Goal Tracking with milestone celebrations
- 📅 Calendar Integration

## 🛠️ Tech Stack

### Backend
- **Framework**: .NET 9.0 (ASP.NET Core Web API)
- **Architecture**: Clean Architecture (Onion/Hexagonal)
- **ORM**: Entity Framework Core 9.0
- **Database**: SQL Server / PostgreSQL
- **Authentication**: JWT Bearer with BCrypt password hashing
- **API Documentation**: Swagger/OpenAPI (Swashbuckle)
- **Logging**: Serilog (planned)
- **Validation**: FluentValidation
- **Mapping**: AutoMapper
- **CQRS**: MediatR

### Frontend
- **Framework**: React 19 with TypeScript 5.9+
- **Build Tool**: Vite
- **Styling**: Tailwind CSS v4
- **Routing**: React Router 6
- **State Management**: Zustand (planned)
- **Data Fetching**: TanStack Query (React Query, planned)
- **Forms**: React Hook Form + Zod (planned)
- **HTTP Client**: Axios
- **Charts**: Chart.js / Recharts (planned)
- **UI Components**: Custom components with Tailwind
- **Animations**: Framer Motion (planned)

### DevOps & Quality
- **Containerization**: Docker with multi-stage builds
- **Orchestration**: Docker Compose
- **Linters**: StyleCop, SonarAnalyzer, Roslynator (C#) | ESLint, Prettier (TypeScript)
- **Pre-commit Hooks**: Husky with automated linting and formatting
- **CI/CD**: GitHub Actions (Backend, Frontend, PR Validation, Dependency Review)
- **Code Coverage**: >80% target (infrastructure ready)
- **Architecture Tests**: NetArchTest.Rules (planned)
- **Testing**: xUnit, Vitest, React Testing Library (planned)

## 📋 Prerequisites

### For Docker (Recommended)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) 20.10+
- [Docker Compose](https://docs.docker.com/compose/install/) 2.0+ (included with Docker Desktop)

### For Local Development
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Node.js 20.x LTS](https://nodejs.org/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or [PostgreSQL](https://www.postgresql.org/)
- [Git](https://git-scm.com/)

### Recommended
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [JetBrains Rider](https://www.jetbrains.com/rider/)
- [VS Code](https://code.visualstudio.com/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (for containerization)

## 🚦 Getting Started

You can run WhatsNext in two ways:

### Option 1: Docker (Recommended) 🐳

**Fastest way to get started!** Everything runs in containers:

```bash
# Clone the repository
git clone https://github.com/mwhayford/WhatsNext.git
cd WhatsNext

# Create environment file
cp env.example .env

# Start with Docker Compose
docker-compose up -d

# Access the application
# Frontend:  http://localhost:3000
# Backend:   http://localhost:5000/api
# Swagger:   http://localhost:5000/swagger
```

📖 **[Complete Docker Documentation](docs/DOCKER.md)**

### Option 2: Local Development

Full local setup with your own database:

### 1. Clone the Repository

```bash
git clone https://github.com/mwhayford/WhatsNext.git
cd WhatsNext
```

### 2. Backend Setup

```bash
cd backend

# Restore dependencies
dotnet restore

# Update database connection string in appsettings.json
# Then run migrations
dotnet ef database update --project src/WhatsNext.Infrastructure --startup-project src/WhatsNext.API

# Run the API
dotnet run --project src/WhatsNext.API
```

The API will be available at `https://localhost:7001` (or the port specified).

### 3. Frontend Setup

```bash
cd frontend

# Install dependencies
npm install

# Create .env.local file
echo "VITE_API_URL=https://localhost:7001" > .env.local

# Run development server
npm run dev
```

The frontend will be available at `http://localhost:5173`.

## 📁 Project Structure

```
WhatsNext/
├── .github/
│   └── workflows/              # CI/CD workflows
├── backend/
│   ├── src/
│   │   ├── WhatsNext.API/              # Presentation Layer
│   │   ├── WhatsNext.Application/      # Application Layer (CQRS, MediatR)
│   │   ├── WhatsNext.Domain/           # Domain Layer (Entities, Value Objects)
│   │   └── WhatsNext.Infrastructure/   # Infrastructure Layer (EF Core, Services)
│   └── tests/
│       ├── WhatsNext.UnitTests/
│       ├── WhatsNext.IntegrationTests/
│       └── WhatsNext.ArchitectureTests/
├── frontend/
│   ├── src/
│   │   ├── components/         # Reusable UI components
│   │   ├── pages/              # Page components
│   │   ├── hooks/              # Custom React hooks
│   │   ├── services/           # API services
│   │   ├── stores/             # State management
│   │   └── types/              # TypeScript types
│   └── tests/
├── docs/
│   ├── architecture/           # Architecture diagrams
│   ├── api/                    # API documentation
│   └── images/                 # Screenshots and images
└── README.md
```

## 🧪 Running Tests

### Backend Tests

```bash
cd backend

# Run all tests
dotnet test

# Run with coverage
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# Run specific test category
dotnet test --filter Category=Unit
dotnet test --filter Category=Integration
```

### Frontend Tests

```bash
cd frontend

# Run unit tests
npm run test

# Run tests with coverage
npm run test:coverage

# Run E2E tests
npm run test:e2e
```

## 🔧 Development

### Code Quality

The project enforces strict code quality standards:

#### Backend
- StyleCop analyzes code style
- SonarAnalyzer checks for bugs and code smells
- Roslynator provides advanced refactoring suggestions
- Architecture tests ensure layer boundaries

Run checks manually:
```bash
dotnet format --verify-no-changes
dotnet build /p:EnforceCodeStyleInBuild=true
```

#### Frontend
- ESLint checks TypeScript/React code
- Prettier formats code consistently
- TypeScript strict mode enabled

Run checks manually:
```bash
npm run lint
npm run format:check
npm run type-check
```

### Pre-commit Hooks

Pre-commit hooks automatically run before each commit:
- Linting (backend and frontend)
- Type checking (frontend)
- Unit tests for changed files

Hooks are configured using Husky.

## 📚 Documentation

- [Project Charter](PROJECT_CHARTER.md) - Comprehensive project plan
- [Architecture Documentation](ARCHITECTURE.md) - System design and decisions
- [Contributing Guidelines](CONTRIBUTING.md) - How to contribute
- [API Documentation](https://localhost:7001/swagger) - Interactive API docs (when running)

## 🏗️ Architecture

This project follows **Clean Architecture** principles with clear separation of concerns:

### Backend Layers
1. **Domain Layer** - Core business logic and entities
2. **Application Layer** - Use cases and application logic (CQRS with MediatR)
3. **Infrastructure Layer** - External concerns (database, external APIs)
4. **Presentation Layer** - API controllers and DTOs

### Frontend Architecture
- **Component-based** architecture with React
- **Custom hooks** for business logic
- **Service layer** for API communication
- **State management** with Zustand for global state
- **React Query** for server state management

For detailed architecture diagrams, see [docs/architecture/](docs/architecture/).

## 🚀 Deployment

### Backend Deployment (Azure)

```bash
# Login to Azure
az login

# Deploy to Azure App Service
dotnet publish -c Release
az webapp deploy --resource-group <rg-name> --name <app-name> --src-path ./publish
```

### Frontend Deployment (Vercel)

```bash
# Install Vercel CLI
npm i -g vercel

# Deploy
cd frontend
vercel --prod
```

See [docs/deployment/](docs/deployment/) for detailed deployment instructions.

## 📊 Performance

- API Response Time: <2s (95th percentile)
- Initial Page Load: <3s
- Lighthouse Score: >90 (all categories)
- Test Coverage: >80%

## 🔐 Security

- JWT authentication with refresh tokens
- Password hashing with BCrypt
- HTTPS enforcement
- CORS configuration
- Rate limiting
- Input validation
- SQL injection prevention
- XSS protection

## 🤝 Contributing

Contributions are welcome! Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct and the process for submitting pull requests.

### Development Workflow

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'feat: add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👤 Author

**Your Name**
- Portfolio: [yourportfolio.com](https://yourportfolio.com)
- LinkedIn: [@yourprofile](https://linkedin.com/in/yourprofile)
- GitHub: [@yourusername](https://github.com/yourusername)

## 🙏 Acknowledgments

- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) by Robert C. Martin
- [Microsoft .NET Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [React Documentation](https://react.dev/)
- [Tailwind CSS](https://tailwindcss.com/)

## 📧 Contact

Have questions or suggestions? Feel free to open an issue or reach out!

---

**Built with ❤️ for the developer community**

