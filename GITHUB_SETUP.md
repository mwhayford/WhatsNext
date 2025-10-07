# GitHub Repository Setup Guide

This guide will help you push your WhatsNext project to GitHub.

## 📝 Prerequisites

- [x] Git repository initialized locally ✅
- [x] Initial commit created ✅
- [ ] GitHub account (create one at https://github.com if you don't have one)
- [ ] Git configured with your credentials

---

## 🔧 Step 1: Configure Git (If Not Already Done)

If you haven't configured Git with your name and email, run these commands:

```bash
git config --global user.name "Your Name"
git config --global user.email "your.email@example.com"
```

To verify:
```bash
git config --global user.name
git config --global user.email
```

---

## 🌐 Step 2: Create GitHub Repository

### Option A: Via GitHub Web Interface (Recommended)

1. **Go to GitHub**: https://github.com/new

2. **Fill in repository details**:
   - **Repository name**: `WhatsNext` (or your preferred name)
   - **Description**: `Personal Productivity Dashboard - Habit Tracker, Task Manager & Pomodoro Timer`
   - **Visibility**: 
     - ✅ **Public** (Recommended for portfolio projects)
     - ⚠️ **Private** (If you prefer to keep it private initially)
   
3. **⚠️ IMPORTANT - Do NOT check these boxes**:
   - ❌ Add a README file (we already have one)
   - ❌ Add .gitignore (we already have one)
   - ❌ Choose a license (we already have MIT in our files)

4. **Click "Create repository"**

5. **Copy the repository URL** from the next page:
   - HTTPS: `https://github.com/yourusername/WhatsNext.git`
   - SSH: `git@github.com:yourusername/WhatsNext.git`

### Option B: Via GitHub CLI (If Installed)

If you have GitHub CLI installed:

```bash
cd C:\src\WhatsNext
gh auth login
gh repo create WhatsNext --public --description "Personal Productivity Dashboard - Full-stack app with C# Web API backend and React frontend" --source=. --remote=origin --push
```

This will automatically create the repo and push your code!

---

## 🚀 Step 3: Add Remote and Push (Manual Method)

If you created the repository via web interface, follow these steps:

### 3.1 Add the Remote

Replace `yourusername` with your actual GitHub username:

**For HTTPS:**
```bash
cd C:\src\WhatsNext
git remote add origin https://github.com/yourusername/WhatsNext.git
```

**For SSH:**
```bash
cd C:\src\WhatsNext
git remote add origin git@github.com:yourusername/WhatsNext.git
```

### 3.2 Verify Remote

```bash
git remote -v
```

You should see:
```
origin  https://github.com/yourusername/WhatsNext.git (fetch)
origin  https://github.com/yourusername/WhatsNext.git (push)
```

### 3.3 Rename Branch to Main (Optional but Recommended)

GitHub uses `main` as the default branch name. Let's rename our `master` branch:

```bash
git branch -M main
```

### 3.4 Push to GitHub

```bash
git push -u origin main
```

If prompted for credentials:
- **Username**: Your GitHub username
- **Password**: Your GitHub Personal Access Token (not your account password!)
  - To create a token: https://github.com/settings/tokens
  - Select scopes: `repo` (Full control of private repositories)

---

## 🎯 Step 4: Verify Upload

1. Go to your repository URL: `https://github.com/yourusername/WhatsNext`
2. You should see:
   - ✅ 88 files
   - ✅ README.md displayed on the homepage
   - ✅ Multiple folders: `backend/`, `frontend/`, `docs/`, `.github/`
   - ✅ Green CI/CD badges (will appear after first workflow run)

---

## 🔄 Step 5: Set Up Branch Protection (Recommended)

Protect your main branch to ensure quality:

1. Go to: `https://github.com/yourusername/WhatsNext/settings/branches`
2. Click "Add rule" or "Add branch protection rule"
3. Branch name pattern: `main`
4. Enable these settings:
   - ✅ Require a pull request before merging
   - ✅ Require status checks to pass before merging
     - Add: `build-and-test` (Backend CI)
     - Add: `build-and-test` (Frontend CI)
   - ✅ Require branches to be up to date before merging
   - ✅ Do not allow bypassing the above settings
5. Click "Create" or "Save changes"

---

## 📊 Step 6: Enable GitHub Actions

GitHub Actions should be enabled automatically. To verify:

1. Go to the "Actions" tab in your repository
2. You should see the workflow files:
   - Backend CI/CD
   - Frontend CI/CD
   - Pull Request Validation
   - Dependency Review

If you see a message about enabling workflows, click "I understand my workflows, go ahead and enable them"

---

## 🏷️ Step 7: Add Topics (Optional but Recommended for Portfolio)

Add topics to make your repository more discoverable:

1. Go to your repository homepage
2. Click the ⚙️ gear icon next to "About"
3. Add topics:
   - `csharp`
   - `dotnet`
   - `aspnet-core`
   - `react`
   - `typescript`
   - `tailwindcss`
   - `clean-architecture`
   - `productivity`
   - `habit-tracker`
   - `pomodoro-timer`
   - `task-manager`
   - `portfolio-project`
4. Update website: (Leave blank for now)
5. Save changes

---

## 🎨 Step 8: Update README Badges

Once your repository is live, update the badges in `README.md`:

```markdown
[![Backend CI/CD](https://github.com/yourusername/WhatsNext/workflows/Backend%20CI%2FCD/badge.svg)](https://github.com/yourusername/WhatsNext/actions)
[![Frontend CI/CD](https://github.com/yourusername/WhatsNext/workflows/Frontend%20CI%2FCD/badge.svg)](https://github.com/yourusername/WhatsNext/actions)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
```

Replace `yourusername` with your actual GitHub username.

---

## 🔑 Authentication Methods

### Option A: HTTPS with Personal Access Token (Recommended)

1. **Generate Token**: https://github.com/settings/tokens
2. Click "Generate new token (classic)"
3. Note: "WhatsNext Repository Access"
4. Expiration: Choose duration (recommend 90 days or no expiration for personal projects)
5. Select scopes:
   - ✅ `repo` (Full control of private repositories)
   - ✅ `workflow` (Update GitHub Action workflows)
6. Click "Generate token"
7. **⚠️ COPY THE TOKEN IMMEDIATELY** (you won't be able to see it again)
8. Use this token as your password when pushing

### Option B: SSH (More Secure)

1. **Generate SSH Key** (if you don't have one):
   ```bash
   ssh-keygen -t ed25519 -C "your.email@example.com"
   ```
   
2. **Start SSH Agent**:
   ```bash
   eval "$(ssh-agent -s)"
   ssh-add ~/.ssh/id_ed25519
   ```

3. **Copy Public Key**:
   ```bash
   cat ~/.ssh/id_ed25519.pub
   ```

4. **Add to GitHub**: 
   - Go to https://github.com/settings/keys
   - Click "New SSH key"
   - Paste your public key
   - Save

5. **Use SSH URL** when adding remote:
   ```bash
   git remote add origin git@github.com:yourusername/WhatsNext.git
   ```

---

## 🔄 Regular Workflow (After Initial Push)

### Making Changes

```bash
# 1. Create a feature branch
git checkout -b feature/your-feature-name

# 2. Make changes and commit
git add .
git commit -m "feat: add your feature"

# 3. Push to GitHub
git push origin feature/your-feature-name

# 4. Create Pull Request on GitHub
# Go to your repository and click "Compare & pull request"

# 5. After PR is merged, update main
git checkout main
git pull origin main
```

---

## 📋 Quick Command Reference

```bash
# Check status
git status

# Check remote
git remote -v

# Check current branch
git branch

# View commit history
git log --oneline

# Pull latest changes
git pull origin main

# Push changes
git push origin main

# Create new branch
git checkout -b branch-name

# Switch branches
git checkout branch-name

# Delete local branch
git branch -d branch-name

# Delete remote branch
git push origin --delete branch-name
```

---

## ❗ Troubleshooting

### Error: "failed to push some refs"

**Solution**: Pull first, then push:
```bash
git pull origin main --rebase
git push origin main
```

### Error: "remote: Support for password authentication was removed"

**Solution**: You're using your GitHub password instead of a Personal Access Token. Generate a token (see Step 8) and use it as your password.

### Error: "Permission denied (publickey)"

**Solution**: Your SSH key isn't set up correctly. Follow the SSH setup instructions in Step 8, Option B.

### Error: "Repository not found"

**Solution**: Check that:
- The repository exists on GitHub
- The remote URL is correct: `git remote -v`
- You have access to the repository

---

## ✅ Verification Checklist

After pushing to GitHub, verify:

- [ ] All files are visible on GitHub
- [ ] README.md displays correctly on repository homepage
- [ ] CI/CD workflows appear in the Actions tab
- [ ] Folder structure is correct (backend/, frontend/, docs/, .github/)
- [ ] .gitignore is working (no node_modules, bin/, obj/ folders visible)
- [ ] Repository description and topics are set
- [ ] Branch protection rules are configured (if desired)
- [ ] Repository is public/private as intended

---

## 🎉 Success!

Once pushed, your repository will be live at:
```
https://github.com/yourusername/WhatsNext
```

You can share this link on your resume, LinkedIn, or portfolio website!

---

## 📞 Need Help?

If you encounter any issues:
1. Check the troubleshooting section above
2. Review GitHub's documentation: https://docs.github.com
3. Check if GitHub is having issues: https://www.githubstatus.com

---

**Next Steps**: After pushing to GitHub, consider:
1. Adding a detailed CONTRIBUTING.md (already created!)
2. Setting up GitHub Pages for documentation
3. Creating release tags when you reach milestones
4. Adding GitHub Discussions for community engagement
5. Enabling Dependabot for automatic dependency updates

