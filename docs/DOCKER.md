# WhatsNext - Docker Deployment Guide

This guide covers running WhatsNext in Docker containers with PostgreSQL database.

## 📋 Prerequisites

- **Docker**: 20.10+
- **Docker Compose**: 2.0+
- **Git**: For cloning the repository

### Install Docker

**Windows:**
- Download [Docker Desktop for Windows](https://www.docker.com/products/docker-desktop)
- Enable WSL 2 backend

**macOS:**
- Download [Docker Desktop for Mac](https://www.docker.com/products/docker-desktop)

**Linux:**
```bash
curl -fsSL https://get.docker.com -o get-docker.sh
sudo sh get-docker.sh
sudo usermod -aG docker $USER
```

## 🚀 Quick Start

### 1. Clone and Configure

```bash
# Clone the repository
git clone https://github.com/mwhayford/WhatsNext.git
cd WhatsNext

# Create environment file
cp env.example .env

# Edit .env with your secure passwords
nano .env  # or use your preferred editor
```

### 2. Build and Run

```bash
# Build and start all services
docker-compose up -d

# View logs
docker-compose logs -f

# Check status
docker-compose ps
```

### 3. Access the Application

- **Frontend**: http://localhost:3000
- **Backend API**: http://localhost:5000/api
- **Swagger UI**: http://localhost:5000/swagger
- **PostgreSQL**: localhost:5432

### 4. Initialize Database

The database migrations will run automatically on first startup. You can verify:

```bash
# Check backend logs
docker-compose logs backend

# Connect to PostgreSQL
docker-compose exec postgres psql -U whatsnext -d whatsnext

# List tables
\dt

# Exit psql
\q
```

## 📦 Docker Services

### Architecture

```
┌─────────────┐
│   Frontend  │  (Nginx + React)
│   :3000     │
└──────┬──────┘
       │
       ▼
┌─────────────┐
│   Backend   │  (ASP.NET Core API)
│   :5000     │
└──────┬──────┘
       │
       ▼
┌─────────────┐
│  PostgreSQL │  (Database)
│   :5432     │
└─────────────┘
```

### Service Details

#### **Frontend** (whatsnext-frontend)
- **Image**: Built from `frontend/Dockerfile`
- **Base**: Node 20 Alpine + Nginx Alpine
- **Port**: 3000 → 80
- **Features**:
  - Multi-stage build (build + serve)
  - Nginx for production serving
  - SPA routing configured
  - API proxy to backend
  - Health check endpoint
  - Non-root user
  - Gzip compression
  - Security headers

#### **Backend** (whatsnext-backend)
- **Image**: Built from `backend/Dockerfile`
- **Base**: .NET 9.0 SDK + ASP.NET Runtime
- **Port**: 5000 → 8080
- **Features**:
  - Multi-stage build (restore, build, publish, run)
  - Entity Framework migrations
  - JWT authentication
  - Swagger/OpenAPI
  - Health check endpoint
  - Non-root user

#### **PostgreSQL** (whatsnext-postgres)
- **Image**: postgres:16-alpine
- **Port**: 5432 → 5432
- **Features**:
  - Persistent volume
  - Health checks
  - Automatic backup ready
  - Performance optimized

## 🔧 Configuration

### Environment Variables

Create a `.env` file in the root directory:

```env
# Database
DB_PASSWORD=your_secure_database_password

# JWT Authentication
JWT_SECRET=your_super_secret_jwt_key_at_least_32_characters

# API URLs
VITE_API_BASE_URL=http://localhost:5000/api

# Ports (optional - defaults shown)
BACKEND_PORT=5000
FRONTEND_PORT=3000
POSTGRES_PORT=5432
```

### appsettings Configuration

The backend uses environment variables that override `appsettings.json`:

- `ConnectionStrings__DefaultConnection`
- `DatabaseProvider`
- `JwtSettings__Secret`
- `JwtSettings__Issuer`
- `JwtSettings__Audience`
- `JwtSettings__ExpiryMinutes`

## 🛠️ Docker Commands

### Start Services

```bash
# Start all services
docker-compose up -d

# Start specific service
docker-compose up -d backend

# Start with build
docker-compose up -d --build

# View startup logs
docker-compose logs -f
```

### Stop Services

```bash
# Stop all services
docker-compose stop

# Stop and remove containers
docker-compose down

# Stop and remove containers + volumes (WARNING: deletes data)
docker-compose down -v
```

### View Logs

```bash
# All services
docker-compose logs -f

# Specific service
docker-compose logs -f backend
docker-compose logs -f frontend
docker-compose logs -f postgres

# Last 100 lines
docker-compose logs --tail=100 backend
```

### Check Status

```bash
# List running containers
docker-compose ps

# Check health
docker-compose ps | grep healthy

# View resource usage
docker stats
```

### Rebuild Services

```bash
# Rebuild all
docker-compose build

# Rebuild specific service
docker-compose build backend

# Force rebuild (no cache)
docker-compose build --no-cache

# Rebuild and restart
docker-compose up -d --build
```

## 🔍 Troubleshooting

### Backend Won't Start

```bash
# Check logs
docker-compose logs backend

# Common issues:
# 1. Database not ready - wait 30 seconds
# 2. Connection string error - check .env
# 3. Migration error - check database

# Restart backend
docker-compose restart backend
```

### Frontend Can't Connect to Backend

```bash
# Check network
docker network inspect whatsnext_whatsnext-network

# Check if backend is healthy
curl http://localhost:5000/health

# Check frontend nginx config
docker-compose exec frontend cat /etc/nginx/conf.d/default.conf
```

### Database Issues

```bash
# Connect to database
docker-compose exec postgres psql -U whatsnext -d whatsnext

# Check tables
\dt

# Check users table
SELECT * FROM "Users";

# View connection info
\conninfo

# Exit
\q
```

### Clear Everything and Start Fresh

```bash
# Stop and remove everything
docker-compose down -v

# Remove images
docker-compose down --rmi all

# Start fresh
docker-compose up -d --build
```

## 📊 Database Management

### Backup Database

```bash
# Backup to file
docker-compose exec postgres pg_dump -U whatsnext whatsnext > backup.sql

# Backup with timestamp
docker-compose exec postgres pg_dump -U whatsnext whatsnext > backup_$(date +%Y%m%d_%H%M%S).sql
```

### Restore Database

```bash
# Restore from backup
docker-compose exec -T postgres psql -U whatsnext whatsnext < backup.sql
```

### Access Database

```bash
# psql
docker-compose exec postgres psql -U whatsnext -d whatsnext

# Using connection string
psql "postgresql://whatsnext:your_password@localhost:5432/whatsnext"
```

## 🚀 Production Deployment

### Security Checklist

- [ ] Change default passwords in `.env`
- [ ] Use strong JWT secret (32+ characters)
- [ ] Enable HTTPS (use reverse proxy)
- [ ] Set `ASPNETCORE_ENVIRONMENT=Production`
- [ ] Configure firewall rules
- [ ] Enable Docker secrets for sensitive data
- [ ] Set up log aggregation
- [ ] Configure backup automation
- [ ] Enable monitoring

### Using Docker Secrets (Production)

```yaml
# docker-compose.prod.yml
secrets:
  db_password:
    file: ./secrets/db_password.txt
  jwt_secret:
    file: ./secrets/jwt_secret.txt

services:
  backend:
    secrets:
      - db_password
      - jwt_secret
    environment:
      - ConnectionStrings__DefaultConnection=Host=postgres;Database=whatsnext;Username=whatsnext;Password_File=/run/secrets/db_password
```

### Reverse Proxy (Nginx)

```nginx
# /etc/nginx/sites-available/whatsnext
server {
    listen 80;
    server_name yourdomain.com;
    
    location / {
        proxy_pass http://localhost:3000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection 'upgrade';
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
    }
}
```

### SSL with Let's Encrypt

```bash
# Install certbot
sudo apt-get install certbot python3-certbot-nginx

# Get certificate
sudo certbot --nginx -d yourdomain.com

# Auto-renewal
sudo certbot renew --dry-run
```

## 📈 Monitoring

### Health Checks

```bash
# Backend health
curl http://localhost:5000/health

# Frontend health
curl http://localhost:3000/health

# Database health
docker-compose exec postgres pg_isready -U whatsnext
```

### View Metrics

```bash
# Container stats
docker stats

# Disk usage
docker system df

# View logs with timestamps
docker-compose logs -f -t
```

## 🔄 Updates and Maintenance

### Update Application

```bash
# Pull latest code
git pull origin main

# Rebuild and restart
docker-compose down
docker-compose up -d --build

# Check status
docker-compose ps
```

### Database Migrations

```bash
# Migrations run automatically on startup
# To run manually:
docker-compose exec backend dotnet ef database update
```

### Clean Up

```bash
# Remove unused images
docker image prune -a

# Remove unused volumes
docker volume prune

# Remove unused networks
docker network prune

# Remove everything unused
docker system prune -a --volumes
```

## 📚 Additional Resources

- [Docker Documentation](https://docs.docker.com/)
- [Docker Compose Reference](https://docs.docker.com/compose/)
- [PostgreSQL Docker Hub](https://hub.docker.com/_/postgres)
- [ASP.NET Core Docker](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/)
- [Nginx Configuration](https://nginx.org/en/docs/)

## ❓ FAQ

**Q: Can I use SQL Server instead of PostgreSQL?**

A: Yes, update `docker-compose.yml` to use `mcr.microsoft.com/mssql/server` and change connection string.

**Q: How do I scale the application?**

A: Use `docker-compose up -d --scale backend=3` to run multiple backend instances. Add a load balancer.

**Q: Where is the data stored?**

A: In Docker volumes. Use `docker volume inspect whatsnext_postgres-data` to see location.

**Q: How do I access logs?**

A: Use `docker-compose logs -f [service_name]` or configure log shipping to external service.

**Q: Can I run this on ARM (Apple Silicon)?**

A: Yes, Docker will automatically use ARM-compatible images.

---

**Need Help?** Check the logs first: `docker-compose logs -f`

