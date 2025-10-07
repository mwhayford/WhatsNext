#!/bin/bash
# WhatsNext - Docker Quick Start Script (Linux/macOS)

echo ""
echo "========================================"
echo "🐳 WhatsNext Docker Deployment"
echo "========================================"
echo ""

# Check if Docker is running
if ! docker ps > /dev/null 2>&1; then
    echo "❌ Docker is not running. Please start Docker first."
    exit 1
fi
echo "✅ Docker is running"

# Check if .env exists
if [ ! -f ".env" ]; then
    echo "⚠️  .env file not found. Creating from template..."
    if [ -f "env.example" ]; then
        cp env.example .env
        echo "✅ Created .env file. Please review and update with your values."
        echo "   Edit: .env"
        echo ""
    else
        echo "❌ env.example not found!"
        exit 1
    fi
fi

# Stop any running services
echo "📦 Stopping any existing containers..."
docker-compose down 2>/dev/null

# Build and start services
echo ""
echo "🏗️  Building Docker images..."
echo "   This may take a few minutes on first run..."
echo ""

if ! docker-compose build; then
    echo "❌ Build failed!"
    exit 1
fi

echo "✅ Build completed!"

echo ""
echo "🚀 Starting services..."
if ! docker-compose up -d; then
    echo "❌ Start failed!"
    exit 1
fi

echo "✅ Services started!"
echo ""

# Wait for services to be healthy
echo "⏳ Waiting for services to be healthy..."
sleep 10

# Check service status
echo ""
echo "📊 Service Status:"
docker-compose ps

echo ""
echo "========================================"
echo "✅ WhatsNext is Running!"
echo "========================================"
echo ""

echo "🌐 Access Points:"
echo "   Frontend:  http://localhost:3000"
echo "   Backend:   http://localhost:5000/api"
echo "   Swagger:   http://localhost:5000/swagger"
echo "   Database:  localhost:5432 (user: whatsnext)"
echo ""

echo "📋 Useful Commands:"
echo "   View logs:     docker-compose logs -f"
echo "   Stop:          docker-compose stop"
echo "   Restart:       docker-compose restart"
echo "   Remove all:    docker-compose down -v"
echo ""

echo "📖 Documentation: docs/DOCKER.md"
echo ""

# Ask if user wants to open browser (Linux/macOS)
read -p "Open application in browser? (y/n) " -n 1 -r
echo ""
if [[ $REPLY =~ ^[Yy]$ ]]; then
    sleep 2
    echo "Opening browser..."
    if command -v xdg-open > /dev/null; then
        xdg-open "http://localhost:3000" 2>/dev/null
        xdg-open "http://localhost:5000/swagger" 2>/dev/null
    elif command -v open > /dev/null; then
        open "http://localhost:3000"
        open "http://localhost:5000/swagger"
    fi
fi

echo ""
echo "✨ Happy coding!"
echo ""

