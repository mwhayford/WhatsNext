# WhatsNext Architecture Documentation

## Overview

WhatsNext follows **Clean Architecture** principles (also known as Onion Architecture or Hexagonal Architecture) to ensure maintainability, testability, and independence from external frameworks.

## Architecture Principles

### Core Principles
1. **Independence**: Business logic independent of UI, database, and external services
2. **Testability**: Easy to test without UI, database, or external dependencies
3. **Separation of Concerns**: Clear boundaries between layers
4. **Dependency Rule**: Dependencies point inward toward the domain

### Dependency Flow
```
API (Presentation) → Infrastructure → Application → Domain
                                                      ↑
                                            (All layers depend on Domain)
```

---

## System Architecture (C4 Model)

### Level 1: System Context Diagram

```mermaid
graph TB
    User[👤 User<br/>Web Browser]
    Dashboard[WhatsNext Dashboard<br/>Personal Productivity System]
    WeatherAPI[☁️ Weather API<br/>OpenWeatherMap]
    QuoteAPI[💭 Quote API<br/>ZenQuotes]
    EmailService[📧 Email Service<br/>SendGrid]
    Database[(🗄️ SQL Server/PostgreSQL<br/>User Data)]
    
    User -->|Uses| Dashboard
    Dashboard -->|Fetches Weather| WeatherAPI
    Dashboard -->|Fetches Quotes| QuoteAPI
    Dashboard -->|Sends Notifications| EmailService
    Dashboard -->|Stores/Retrieves| Database
    
    style Dashboard fill:#3B82F6,stroke:#1E40AF,stroke-width:3px,color:#fff
    style User fill:#10B981,stroke:#059669,color:#fff
    style Database fill:#F59E0B,stroke:#D97706,color:#fff
```

### Level 2: Container Diagram

```mermaid
graph TB
    User[👤 User]
    
    subgraph "WhatsNext System"
        WebApp[React SPA<br/>TypeScript + Tailwind<br/>Port 5173]
        API[ASP.NET Core API<br/>RESTful API<br/>Port 7001]
        DB[(SQL Server/PostgreSQL<br/>User Data & Sessions)]
        Cache[(Redis Cache<br/>Optional)]
    end
    
    WeatherAPI[☁️ Weather API]
    QuoteAPI[💭 Quote API]
    
    User -->|HTTPS| WebApp
    WebApp -->|JSON/HTTPS| API
    API -->|Entity Framework Core| DB
    API -->|Optional Caching| Cache
    API -->|HTTP| WeatherAPI
    API -->|HTTP| QuoteAPI
    
    style WebApp fill:#3B82F6,stroke:#1E40AF,stroke-width:2px,color:#fff
    style API fill:#10B981,stroke:#059669,stroke-width:2px,color:#fff
    style DB fill:#F59E0B,stroke:#D97706,stroke-width:2px,color:#fff
```

### Level 3: Component Diagram (Backend)

```mermaid
graph TB
    subgraph "Presentation Layer (WhatsNext.API)"
        Controllers[Controllers<br/>AuthController<br/>HabitsController<br/>TasksController<br/>TimerController]
        Middleware[Middleware<br/>Exception Handler<br/>JWT Authentication<br/>Rate Limiting]
        Filters[Filters<br/>Validation<br/>Authorization]
    end
    
    subgraph "Application Layer (WhatsNext.Application)"
        Commands[Commands<br/>CreateHabitCommand<br/>CompleteTaskCommand]
        Queries[Queries<br/>GetHabitsQuery<br/>GetTasksQuery]
        Handlers[MediatR Handlers<br/>Command Handlers<br/>Query Handlers]
        Validators[FluentValidation<br/>Validators]
        Mappings[AutoMapper<br/>Profiles]
    end
    
    subgraph "Domain Layer (WhatsNext.Domain)"
        Entities[Entities<br/>User, Habit, Task<br/>TimerSession, Quote]
        ValueObjects[Value Objects]
        Enums[Enums<br/>TaskStatus<br/>HabitFrequency]
        DomainEvents[Domain Events]
    end
    
    subgraph "Infrastructure Layer (WhatsNext.Infrastructure)"
        DbContext[ApplicationDbContext<br/>EF Core]
        Repositories[Repositories<br/>HabitRepository<br/>TaskRepository]
        Services[External Services<br/>WeatherService<br/>EmailService]
    end
    
    Controllers --> Middleware
    Controllers --> Handlers
    Handlers --> Validators
    Handlers --> Repositories
    Handlers --> Mappings
    Handlers --> Entities
    Repositories --> DbContext
    DbContext --> Entities
    Services --> Entities
    
    style Controllers fill:#3B82F6,stroke:#1E40AF,color:#fff
    style Handlers fill:#10B981,stroke:#059669,color:#fff
    style Entities fill:#F59E0B,stroke:#D97706,color:#fff
    style Repositories fill:#8B5CF6,stroke:#6D28D9,color:#fff
```

---

## Backend Architecture

### Clean Architecture Layers

#### 1. Domain Layer (Core)
**Location**: `backend/src/WhatsNext.Domain/`

**Responsibilities**:
- Define business entities
- Define value objects
- Define domain events
- Define enums and exceptions
- Contains NO external dependencies

**Structure**:
```
WhatsNext.Domain/
├── Common/
│   ├── BaseEntity.cs
│   └── AuditableEntity.cs
├── Entities/
│   ├── User.cs
│   ├── Habit.cs
│   ├── HabitCompletion.cs
│   ├── TodoTask.cs
│   ├── TimerSession.cs
│   └── Quote.cs
├── ValueObjects/
├── Enums/
│   ├── HabitFrequency.cs
│   ├── TaskPriority.cs
│   ├── TaskStatus.cs
│   └── SessionType.cs
├── Events/
└── Exceptions/
```

**Dependencies**: None (Pure C# / .NET)

#### 2. Application Layer
**Location**: `backend/src/WhatsNext.Application/`

**Responsibilities**:
- Define use cases (Commands and Queries)
- Implement CQRS pattern with MediatR
- Define interfaces for infrastructure
- Implement validation logic
- Define DTOs and mapping profiles

**Structure**:
```
WhatsNext.Application/
├── Common/
│   ├── Interfaces/
│   │   ├── IApplicationDbContext.cs
│   │   ├── IDateTime.cs
│   │   └── ICurrentUserService.cs
│   ├── Models/
│   │   ├── Result.cs
│   │   └── PaginatedList.cs
│   └── Behaviors/
│       ├── ValidationBehavior.cs
│       └── LoggingBehavior.cs
├── Features/
│   ├── Auth/
│   │   ├── Commands/
│   │   │   ├── LoginCommand.cs
│   │   │   └── RegisterCommand.cs
│   │   └── Queries/
│   ├── Habits/
│   │   ├── Commands/
│   │   │   ├── CreateHabitCommand.cs
│   │   │   ├── UpdateHabitCommand.cs
│   │   │   ├── DeleteHabitCommand.cs
│   │   │   └── CompleteHabitCommand.cs
│   │   └── Queries/
│   │       ├── GetHabitsQuery.cs
│   │       └── GetHabitByIdQuery.cs
│   ├── Tasks/
│   ├── Timer/
│   └── Quotes/
├── Mappings/
│   └── MappingProfile.cs
└── DependencyInjection.cs
```

**Dependencies**: Domain Layer, MediatR, FluentValidation, AutoMapper

#### 3. Infrastructure Layer
**Location**: `backend/src/WhatsNext.Infrastructure/`

**Responsibilities**:
- Implement data access (EF Core)
- Implement external service integrations
- Implement repositories
- Configure database migrations

**Structure**:
```
WhatsNext.Infrastructure/
├── Persistence/
│   ├── Contexts/
│   │   └── ApplicationDbContext.cs
│   ├── Configurations/
│   │   ├── UserConfiguration.cs
│   │   ├── HabitConfiguration.cs
│   │   └── TaskConfiguration.cs
│   ├── Repositories/
│   │   ├── HabitRepository.cs
│   │   └── TaskRepository.cs
│   ├── Migrations/
│   └── Seeders/
│       └── DataSeeder.cs
├── Services/
│   ├── DateTimeService.cs
│   ├── WeatherService.cs
│   ├── EmailService.cs
│   └── QuoteService.cs
└── DependencyInjection.cs
```

**Dependencies**: Application Layer, Domain Layer, EF Core, External APIs

#### 4. Presentation Layer (API)
**Location**: `backend/src/WhatsNext.API/`

**Responsibilities**:
- Expose HTTP endpoints
- Handle authentication and authorization
- Implement middleware
- Configure Swagger/OpenAPI
- Handle errors globally

**Structure**:
```
WhatsNext.API/
├── Controllers/
│   ├── AuthController.cs
│   ├── HabitsController.cs
│   ├── TasksController.cs
│   ├── TimerController.cs
│   └── QuotesController.cs
├── Middleware/
│   ├── ExceptionHandlingMiddleware.cs
│   └── PerformanceMiddleware.cs
├── Filters/
│   ├── ApiExceptionFilterAttribute.cs
│   └── ValidateModelAttribute.cs
├── Extensions/
│   └── ServiceCollectionExtensions.cs
├── appsettings.json
├── appsettings.Development.json
└── Program.cs
```

**Dependencies**: Application Layer, Infrastructure Layer

---

## Frontend Architecture

### Component Hierarchy

```mermaid
graph TB
    App[App.tsx<br/>Router Setup]
    
    App --> Layout[MainLayout]
    App --> Auth[Auth Pages]
    
    Layout --> Header[Header<br/>Navigation]
    Layout --> Sidebar[Sidebar<br/>Menu]
    Layout --> Main[Main Content]
    
    Main --> Dashboard[Dashboard Page]
    Main --> Habits[Habits Page]
    Main --> Tasks[Tasks Page]
    Main --> Timer[Timer Page]
    
    Dashboard --> QuoteWidget[Quote Widget]
    Dashboard --> WeatherWidget[Weather Widget]
    Dashboard --> StatsWidget[Stats Widget]
    
    Habits --> HabitList[Habit List]
    HabitList --> HabitCard[Habit Card]
    HabitCard --> HabitForm[Habit Form]
    
    Tasks --> TaskList[Task List]
    TaskList --> TaskCard[Task Card]
    
    Timer --> PomodoroTimer[Pomodoro Timer]
    Timer --> SessionHistory[Session History]
    
    style App fill:#3B82F6,stroke:#1E40AF,color:#fff
    style Layout fill:#10B981,stroke:#059669,color:#fff
    style Dashboard fill:#F59E0B,stroke:#D97706,color:#fff
```

### Frontend Structure

```
frontend/
├── src/
│   ├── components/
│   │   ├── common/
│   │   │   ├── Button/
│   │   │   ├── Input/
│   │   │   ├── Modal/
│   │   │   ├── Card/
│   │   │   └── LoadingSpinner/
│   │   ├── layout/
│   │   │   ├── Header/
│   │   │   ├── Sidebar/
│   │   │   ├── Footer/
│   │   │   └── MainLayout/
│   │   └── features/
│   │       ├── habits/
│   │       │   ├── HabitCard.tsx
│   │       │   ├── HabitList.tsx
│   │       │   ├── HabitForm.tsx
│   │       │   └── HabitCalendar.tsx
│   │       ├── tasks/
│   │       ├── timer/
│   │       └── quotes/
│   ├── pages/
│   │   ├── Dashboard/
│   │   ├── Habits/
│   │   ├── Tasks/
│   │   ├── Timer/
│   │   └── Auth/
│   │       ├── Login.tsx
│   │       └── Register.tsx
│   ├── hooks/
│   │   ├── useAuth.ts
│   │   ├── useHabits.ts
│   │   ├── useTasks.ts
│   │   └── useTimer.ts
│   ├── services/
│   │   ├── api.ts
│   │   └── endpoints/
│   │       ├── auth.ts
│   │       ├── habits.ts
│   │       ├── tasks.ts
│   │       └── timer.ts
│   ├── stores/
│   │   ├── authStore.ts
│   │   └── uiStore.ts
│   ├── types/
│   │   ├── habit.ts
│   │   ├── task.ts
│   │   ├── user.ts
│   │   └── timer.ts
│   ├── utils/
│   │   ├── formatters.ts
│   │   ├── validators.ts
│   │   └── constants.ts
│   ├── styles/
│   │   └── global.css
│   ├── App.tsx
│   └── main.tsx
```

---

## Data Flow

### CQRS Pattern

```mermaid
sequenceDiagram
    participant Client as React Client
    participant API as API Controller
    participant Handler as MediatR Handler
    participant Repo as Repository
    participant DB as Database
    
    Note over Client,DB: Command Flow (Write)
    Client->>API: POST /api/habits
    API->>Handler: Send CreateHabitCommand
    Handler->>Handler: Validate with FluentValidation
    Handler->>Repo: SaveAsync(habit)
    Repo->>DB: INSERT INTO Habits
    DB-->>Repo: Success
    Repo-->>Handler: Habit Entity
    Handler-->>API: HabitDto
    API-->>Client: 201 Created
    
    Note over Client,DB: Query Flow (Read)
    Client->>API: GET /api/habits
    API->>Handler: Send GetHabitsQuery
    Handler->>Repo: GetAllAsync()
    Repo->>DB: SELECT * FROM Habits
    DB-->>Repo: Habit Entities
    Repo-->>Handler: List<Habit>
    Handler-->>API: List<HabitDto>
    API-->>Client: 200 OK
```

### Authentication Flow

```mermaid
sequenceDiagram
    participant Client
    participant API
    participant AuthService
    participant DB
    
    Client->>API: POST /api/auth/login
    API->>AuthService: ValidateCredentials
    AuthService->>DB: FindUserByEmail
    DB-->>AuthService: User
    AuthService->>AuthService: VerifyPassword
    AuthService->>AuthService: GenerateJWT
    AuthService->>AuthService: GenerateRefreshToken
    AuthService->>DB: SaveRefreshToken
    AuthService-->>API: JWT + RefreshToken
    API-->>Client: 200 OK {token, refreshToken}
    
    Note over Client: Store tokens
    
    Client->>API: GET /api/habits (with JWT)
    API->>API: Validate JWT
    API->>DB: GetHabits for User
    DB-->>API: Habits
    API-->>Client: 200 OK {habits}
```

---

## Database Schema

### Entity Relationship Diagram

```mermaid
erDiagram
    USER ||--o{ HABIT : has
    USER ||--o{ TODO_TASK : has
    USER ||--o{ TIMER_SESSION : has
    USER }o--o{ QUOTE : favorites
    HABIT ||--o{ HABIT_COMPLETION : has
    
    USER {
        int Id PK
        string Email UK
        string PasswordHash
        string FirstName
        string LastName
        bool EmailConfirmed
        string RefreshToken
        datetime RefreshTokenExpiry
        string TimeZone
        datetime CreatedAt
        datetime UpdatedAt
        bool IsDeleted
    }
    
    HABIT {
        int Id PK
        int UserId FK
        string Name
        string Description
        int Frequency
        string Color
        string Icon
        int TargetCount
        int CurrentStreak
        int LongestStreak
        datetime StartDate
        bool IsActive
        datetime CreatedAt
        datetime UpdatedAt
        bool IsDeleted
    }
    
    HABIT_COMPLETION {
        int Id PK
        int HabitId FK
        datetime CompletedDate
        string Notes
        datetime CreatedAt
    }
    
    TODO_TASK {
        int Id PK
        int UserId FK
        string Title
        string Description
        int Priority
        int Status
        datetime DueDate
        datetime CompletedAt
        bool IsImportant
        string Tags
        datetime CreatedAt
        datetime UpdatedAt
        bool IsDeleted
    }
    
    TIMER_SESSION {
        int Id PK
        int UserId FK
        int SessionType
        int DurationMinutes
        datetime StartTime
        datetime EndTime
        bool IsCompleted
        string Notes
        datetime CreatedAt
    }
    
    QUOTE {
        int Id PK
        string Text
        string Author
        string Category
        string Source
        datetime CreatedAt
    }
```

---

## Security Architecture

### Authentication & Authorization

1. **JWT-based Authentication**
   - Access tokens (short-lived: 15 minutes)
   - Refresh tokens (long-lived: 7 days)
   - Tokens stored in httpOnly cookies (frontend)

2. **Password Security**
   - BCrypt hashing with salt
   - Minimum complexity requirements
   - Rate limiting on login attempts

3. **API Security**
   - HTTPS enforcement
   - CORS configuration
   - Rate limiting per IP/user
   - Input validation
   - SQL injection prevention (EF Core parameterization)
   - XSS protection

### Security Measures

```mermaid
graph LR
    Client[Client]
    
    subgraph "Security Layers"
        HTTPS[HTTPS/TLS]
        CORS[CORS Policy]
        RateLimit[Rate Limiting]
        JWT[JWT Validation]
        Input[Input Validation]
        Auth[Authorization]
    end
    
    API[API Endpoint]
    
    Client --> HTTPS
    HTTPS --> CORS
    CORS --> RateLimit
    RateLimit --> JWT
    JWT --> Input
    Input --> Auth
    Auth --> API
    
    style HTTPS fill:#EF4444,stroke:#DC2626,color:#fff
    style JWT fill:#F59E0B,stroke:#D97706,color:#fff
    style Auth fill:#10B981,stroke:#059669,color:#fff
```

---

## Deployment Architecture

### Development Environment
```
Local Machine
├── Backend: https://localhost:7001
├── Frontend: http://localhost:5173
└── Database: LocalDB / Docker PostgreSQL
```

### Production Environment
```
Cloud Infrastructure
├── Frontend: Vercel (Static Hosting + CDN)
├── Backend: Azure App Service / AWS ECS
├── Database: Azure SQL / AWS RDS PostgreSQL
├── Cache: Azure Redis / AWS ElastiCache (Optional)
└── Monitoring: Application Insights / CloudWatch
```

### CI/CD Pipeline

```mermaid
graph LR
    Dev[Developer] -->|Push| GitHub[GitHub Repository]
    GitHub -->|Trigger| Actions[GitHub Actions]
    
    Actions --> Build[Build & Test]
    Build --> Lint[Lint & Format Check]
    Lint --> Test[Run Tests]
    Test --> Coverage[Code Coverage]
    Coverage --> Quality[Quality Gate]
    
    Quality -->|Pass| Deploy{Environment}
    Deploy -->|main branch| Prod[Production]
    Deploy -->|develop branch| Staging[Staging]
    
    Prod --> Monitor[Monitoring & Alerts]
    
    style GitHub fill:#3B82F6,stroke:#1E40AF,color:#fff
    style Quality fill:#10B981,stroke:#059669,color:#fff
    style Prod fill:#EF4444,stroke:#DC2626,color:#fff
```

---

## Performance Considerations

### Backend Optimization
- **Caching**: Redis for frequently accessed data
- **Database Indexing**: On UserId, Email, foreign keys
- **Pagination**: Implement cursor-based pagination
- **Async/Await**: All I/O operations are async
- **Connection Pooling**: EF Core connection pooling

### Frontend Optimization
- **Code Splitting**: Route-based lazy loading
- **Image Optimization**: WebP format, lazy loading
- **Bundle Size**: Tree shaking, minification
- **Caching**: Service Worker for PWA
- **Virtual Scrolling**: For large lists

---

## Monitoring & Observability

### Metrics to Track
- API response times (p50, p95, p99)
- Error rates by endpoint
- Database query performance
- User activity patterns
- Feature usage statistics

### Logging Strategy
- **Structured Logging**: Serilog with JSON output
- **Log Levels**: Debug, Information, Warning, Error, Critical
- **Correlation IDs**: Track requests across services
- **Sensitive Data**: Mask PII in logs

---

## Testing Strategy

### Backend Testing Pyramid
```
        /\
       /  \  E2E Tests (Playwright)
      /    \
     /      \  Integration Tests (API + DB)
    /        \
   /          \  Unit Tests (Domain + Application)
  /____________\
```

### Test Coverage Goals
- **Domain Layer**: 100% (pure business logic)
- **Application Layer**: >90% (CQRS handlers)
- **Infrastructure Layer**: >70% (repository implementations)
- **API Layer**: >80% (endpoint integration tests)

---

## Future Enhancements

### Phase 2
- Real-time updates (SignalR/WebSockets)
- Offline-first PWA capabilities
- Advanced analytics dashboard
- Machine learning habit predictions

### Phase 3
- Mobile apps (React Native)
- Third-party integrations (Google Calendar, Todoist)
- Team/Family sharing features
- Gamification elements

---

## Architectural Decision Records (ADRs)

### ADR-001: Choose Clean Architecture
**Status**: Accepted  
**Context**: Need maintainable, testable codebase  
**Decision**: Implement Clean Architecture with CQRS  
**Consequences**: Improved testability, clear separation of concerns, steeper learning curve

### ADR-002: Use EF Core over Dapper
**Status**: Accepted  
**Context**: Need ORM for data access  
**Decision**: Use EF Core for productivity and migrations  
**Consequences**: Easier development, potential performance trade-offs

### ADR-003: JWT Authentication
**Status**: Accepted  
**Context**: Need stateless authentication  
**Decision**: Implement JWT with refresh tokens  
**Consequences**: Scalable, stateless, requires token management

---

## References

- [Clean Architecture by Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Microsoft .NET Architecture Guides](https://docs.microsoft.com/en-us/dotnet/architecture/)
- [C4 Model for Software Architecture](https://c4model.com/)
- [CQRS Pattern](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)

---

**Last Updated**: October 7, 2025  
**Version**: 1.0  
**Maintained By**: Development Team

