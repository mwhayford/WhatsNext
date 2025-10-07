# WhatsNext - Docker Quick Start Script

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "🐳 WhatsNext Docker Deployment" -ForegroundColor Green
Write-Host "========================================`n" -ForegroundColor Cyan

# Check if Docker is running
try {
    docker ps | Out-Null
    Write-Host "✅ Docker is running" -ForegroundColor Green
} catch {
    Write-Host "❌ Docker is not running. Please start Docker Desktop first." -ForegroundColor Red
    exit 1
}

# Check if .env exists
if (-not (Test-Path ".env")) {
    Write-Host "⚠️  .env file not found. Creating from template..." -ForegroundColor Yellow
    if (Test-Path "env.example") {
        Copy-Item "env.example" ".env"
        Write-Host "✅ Created .env file. Please review and update with your values." -ForegroundColor Green
        Write-Host "   Edit: .env`n" -ForegroundColor Gray
    } else {
        Write-Host "❌ env.example not found!" -ForegroundColor Red
        exit 1
    }
}

# Stop any running services
Write-Host "📦 Stopping any existing containers..." -ForegroundColor Yellow
docker-compose down 2>$null

# Build and start services
Write-Host "`n🏗️  Building Docker images..." -ForegroundColor Yellow
Write-Host "   This may take a few minutes on first run...`n" -ForegroundColor Gray

$buildResult = docker-compose build 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Build failed!" -ForegroundColor Red
    Write-Host $buildResult
    exit 1
}

Write-Host "✅ Build completed!" -ForegroundColor Green

Write-Host "`n🚀 Starting services..." -ForegroundColor Yellow
$startResult = docker-compose up -d 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Start failed!" -ForegroundColor Red
    Write-Host $startResult
    exit 1
}

Write-Host "✅ Services started!`n" -ForegroundColor Green

# Wait for services to be healthy
Write-Host "⏳ Waiting for services to be healthy..." -ForegroundColor Yellow
Start-Sleep -Seconds 10

# Check service status
Write-Host "`n📊 Service Status:" -ForegroundColor Cyan
docker-compose ps

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "✅ WhatsNext is Running!" -ForegroundColor Green
Write-Host "========================================`n" -ForegroundColor Cyan

Write-Host "🌐 Access Points:" -ForegroundColor Yellow
Write-Host "   Frontend:  http://localhost:3000" -ForegroundColor Cyan
Write-Host "   Backend:   http://localhost:5000/api" -ForegroundColor Cyan
Write-Host "   Swagger:   http://localhost:5000/swagger" -ForegroundColor Cyan
Write-Host "   Database:  localhost:5432 (user: whatsnext)`n" -ForegroundColor Cyan

Write-Host "📋 Useful Commands:" -ForegroundColor Yellow
Write-Host "   View logs:     docker-compose logs -f" -ForegroundColor Gray
Write-Host "   Stop:          docker-compose stop" -ForegroundColor Gray
Write-Host "   Restart:       docker-compose restart" -ForegroundColor Gray
Write-Host "   Remove all:    docker-compose down -v`n" -ForegroundColor Gray

Write-Host "📖 Documentation: docs/DOCKER.md`n" -ForegroundColor Gray

# Ask if user wants to open browser
$openBrowser = Read-Host "Open application in browser? (y/n)"
if ($openBrowser -eq "y") {
    Start-Sleep -Seconds 2
    Write-Host "`nOpening browser..." -ForegroundColor Gray
    Start-Process "http://localhost:3000"
    Start-Process "http://localhost:5000/swagger"
}

Write-Host "`n✨ Happy coding!`n" -ForegroundColor Green

