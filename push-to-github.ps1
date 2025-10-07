# WhatsNext - Push to GitHub Script
# This script helps you push your code to GitHub

Write-Host "🚀 WhatsNext - GitHub Push Helper" -ForegroundColor Cyan
Write-Host "=================================" -ForegroundColor Cyan
Write-Host ""

# Check if we're in a git repository
if (-not (Test-Path .git)) {
    Write-Host "❌ Error: Not in a git repository!" -ForegroundColor Red
    exit 1
}

# Check if we have commits
$hasCommits = git rev-parse HEAD 2>$null
if (-not $hasCommits) {
    Write-Host "❌ Error: No commits found. Please make an initial commit first." -ForegroundColor Red
    exit 1
}

Write-Host "✅ Git repository detected" -ForegroundColor Green
Write-Host ""

# Check for existing remote
$existingRemote = git remote get-url origin 2>$null
if ($existingRemote) {
    Write-Host "⚠️  Remote 'origin' already exists: $existingRemote" -ForegroundColor Yellow
    Write-Host ""
    $continue = Read-Host "Do you want to update the remote URL? (y/n)"
    if ($continue -ne "y") {
        Write-Host "Aborting..." -ForegroundColor Yellow
        exit 0
    }
}

Write-Host "📝 Please enter your GitHub repository URL:" -ForegroundColor Cyan
Write-Host "   Example: https://github.com/yourusername/WhatsNext.git" -ForegroundColor Gray
Write-Host "   Or SSH: git@github.com:yourusername/WhatsNext.git" -ForegroundColor Gray
Write-Host ""
$repoUrl = Read-Host "Repository URL"

if (-not $repoUrl) {
    Write-Host "❌ Error: Repository URL cannot be empty!" -ForegroundColor Red
    exit 1
}

# Validate URL format
if ($repoUrl -notmatch "(https://github.com/|git@github.com:)") {
    Write-Host "⚠️  Warning: URL doesn't look like a GitHub URL. Continue anyway? (y/n)" -ForegroundColor Yellow
    $continue = Read-Host
    if ($continue -ne "y") {
        Write-Host "Aborting..." -ForegroundColor Yellow
        exit 0
    }
}

# Add or update remote
Write-Host ""
Write-Host "🔧 Configuring remote..." -ForegroundColor Cyan
if ($existingRemote) {
    git remote set-url origin $repoUrl
    Write-Host "✅ Updated remote 'origin' to: $repoUrl" -ForegroundColor Green
} else {
    git remote add origin $repoUrl
    Write-Host "✅ Added remote 'origin': $repoUrl" -ForegroundColor Green
}

# Check current branch
$currentBranch = git branch --show-current
Write-Host "📍 Current branch: $currentBranch" -ForegroundColor Cyan

# Rename to main if currently master
if ($currentBranch -eq "master") {
    Write-Host ""
    Write-Host "💡 GitHub recommends using 'main' as the default branch name." -ForegroundColor Yellow
    $rename = Read-Host "Rename 'master' to 'main'? (y/n)"
    if ($rename -eq "y") {
        git branch -M main
        $currentBranch = "main"
        Write-Host "✅ Renamed branch to 'main'" -ForegroundColor Green
    }
}

# Show what will be pushed
Write-Host ""
Write-Host "📦 Ready to push:" -ForegroundColor Cyan
git log --oneline -5
Write-Host ""

# Confirm push
Write-Host "🚀 Ready to push to GitHub!" -ForegroundColor Green
Write-Host "   Remote: $repoUrl" -ForegroundColor Gray
Write-Host "   Branch: $currentBranch" -ForegroundColor Gray
Write-Host ""
$push = Read-Host "Proceed with push? (y/n)"

if ($push -ne "y") {
    Write-Host "Aborting push. Your local repository is unchanged." -ForegroundColor Yellow
    exit 0
}

# Push to GitHub
Write-Host ""
Write-Host "🚀 Pushing to GitHub..." -ForegroundColor Cyan
git push -u origin $currentBranch

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "✅ Successfully pushed to GitHub!" -ForegroundColor Green
    Write-Host ""
    Write-Host "🎉 Your repository is now live!" -ForegroundColor Cyan
    Write-Host ""
    
    # Extract username and repo name from URL
    if ($repoUrl -match "github.com[:/]([^/]+)/([^/\.]+)") {
        $username = $matches[1]
        $repoName = $matches[2]
        $repoLink = "https://github.com/$username/$repoName"
        Write-Host "🔗 View your repository: $repoLink" -ForegroundColor Cyan
        Write-Host ""
        Write-Host "📋 Next steps:" -ForegroundColor Yellow
        Write-Host "   1. Visit your repository and verify all files are present" -ForegroundColor Gray
        Write-Host "   2. Check the 'Actions' tab to see CI/CD workflows" -ForegroundColor Gray
        Write-Host "   3. Add repository topics for better discoverability" -ForegroundColor Gray
        Write-Host "   4. Update README.md badges with your username" -ForegroundColor Gray
        Write-Host "   5. Set up branch protection rules (recommended)" -ForegroundColor Gray
    }
} else {
    Write-Host ""
    Write-Host "❌ Push failed!" -ForegroundColor Red
    Write-Host ""
    Write-Host "Common issues:" -ForegroundColor Yellow
    Write-Host "   • Authentication failed - Use a Personal Access Token, not your password" -ForegroundColor Gray
    Write-Host "   • Repository doesn't exist - Create it on GitHub first" -ForegroundColor Gray
    Write-Host "   • No permission - Check repository access rights" -ForegroundColor Gray
    Write-Host ""
    Write-Host "📖 For detailed help, see: GITHUB_SETUP.md" -ForegroundColor Cyan
    exit 1
}

