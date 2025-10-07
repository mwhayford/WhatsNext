# Contributing to WhatsNext

First off, thank you for considering contributing to WhatsNext! It's people like you that make WhatsNext such a great tool.

## Code of Conduct

This project and everyone participating in it is governed by our Code of Conduct. By participating, you are expected to uphold this code. Please report unacceptable behavior to the project maintainers.

## How Can I Contribute?

### Reporting Bugs

Before creating bug reports, please check the issue list as you might find out that you don't need to create one. When you are creating a bug report, please include as many details as possible:

* **Use a clear and descriptive title**
* **Describe the exact steps to reproduce the problem**
* **Provide specific examples to demonstrate the steps**
* **Describe the behavior you observed after following the steps**
* **Explain which behavior you expected to see instead and why**
* **Include screenshots and animated GIFs** if possible

### Suggesting Enhancements

Enhancement suggestions are tracked as GitHub issues. When creating an enhancement suggestion, please include:

* **Use a clear and descriptive title**
* **Provide a step-by-step description of the suggested enhancement**
* **Provide specific examples to demonstrate the steps**
* **Describe the current behavior and explain which behavior you expected to see instead**
* **Explain why this enhancement would be useful**

### Pull Requests

* Fill in the required template
* Follow the C# and TypeScript styleguides
* Include thoughtfully-worded, well-structured tests
* Document new code
* End all files with a newline

## Development Workflow

### 1. Fork and Clone

```bash
# Fork the repository on GitHub
# Clone your fork
git clone https://github.com/YOUR_USERNAME/whatsnext.git
cd whatsnext

# Add upstream remote
git remote add upstream https://github.com/ORIGINAL_OWNER/whatsnext.git
```

### 2. Create a Branch

```bash
# Update your fork
git checkout develop
git pull upstream develop

# Create a feature branch
git checkout -b feature/your-feature-name
# or for bugs
git checkout -b bugfix/your-bug-fix
```

### 3. Make Your Changes

Follow the coding standards and guidelines below.

### 4. Test Your Changes

#### Backend
```bash
cd backend
dotnet test
dotnet format --verify-no-changes
```

#### Frontend
```bash
cd frontend
npm run lint
npm run type-check
npm run test
```

### 5. Commit Your Changes

We follow the [Conventional Commits](https://www.conventionalcommits.org/) specification:

```bash
# Format: <type>(<scope>): <subject>

# Types:
# feat: A new feature
# fix: A bug fix
# docs: Documentation only changes
# style: Changes that do not affect the meaning of the code
# refactor: A code change that neither fixes a bug nor adds a feature
# perf: A code change that improves performance
# test: Adding missing tests or correcting existing tests
# chore: Changes to the build process or auxiliary tools

# Examples:
git commit -m "feat(habits): add streak calculation"
git commit -m "fix(auth): resolve token refresh issue"
git commit -m "docs: update API documentation"
git commit -m "test(tasks): add unit tests for task service"
```

### 6. Push and Create Pull Request

```bash
git push origin feature/your-feature-name
```

Then create a Pull Request on GitHub with:
* Clear title and description
* Reference to related issues
* Screenshots for UI changes
* Test results

## Coding Standards

### C# Backend

#### Naming Conventions

* **Classes, Methods, Properties**: PascalCase
  ```csharp
  public class HabitService
  {
      public async Task<Habit> GetHabitByIdAsync(int id)
  }
  ```

* **Private Fields**: Underscore prefix + camelCase
  ```csharp
  private readonly IHabitRepository _habitRepository;
  ```

* **Local Variables, Parameters**: camelCase
  ```csharp
  public void ProcessHabit(Habit habit)
  {
      var habitName = habit.Name;
  }
  ```

* **Interfaces**: Prefix with 'I'
  ```csharp
  public interface IHabitRepository { }
  ```

* **Constants**: PascalCase
  ```csharp
  public const int MaxHabitsPerUser = 50;
  ```

#### Code Style

* Use `var` when type is obvious
* Use meaningful names, avoid abbreviations
* Keep methods small and focused (Single Responsibility)
* Use async/await for I/O operations
* Place opening braces on new lines
* Use 4 spaces for indentation

#### XML Documentation

Document all public APIs:

```csharp
/// <summary>
/// Creates a new habit for the specified user.
/// </summary>
/// <param name="userId">The ID of the user.</param>
/// <param name="habit">The habit to create.</param>
/// <returns>The created habit.</returns>
/// <exception cref="ArgumentNullException">Thrown when habit is null.</exception>
public async Task<Habit> CreateHabitAsync(int userId, Habit habit)
{
    // Implementation
}
```

#### Architecture Rules

* Domain layer must not reference other layers
* Application layer depends only on Domain
* Infrastructure implements Application interfaces
* API layer is thin, delegates to Application
* Use MediatR for CQRS
* Use Repository pattern for data access

### TypeScript/React Frontend

#### Naming Conventions

* **Components**: PascalCase
  ```typescript
  export const HabitCard: React.FC<HabitCardProps> = ({ habit }) => {
  ```

* **Files**: PascalCase for components, camelCase for utilities
  ```
  HabitCard.tsx
  useHabits.ts
  formatDate.ts
  ```

* **Functions, Variables**: camelCase
  ```typescript
  const handleSubmit = () => { }
  const habitName = 'Exercise';
  ```

* **Interfaces/Types**: PascalCase, prefix interfaces with 'I' (optional)
  ```typescript
  interface HabitCardProps {
      habit: Habit;
  }
  ```

* **Constants**: UPPER_SNAKE_CASE
  ```typescript
  const MAX_HABITS = 50;
  const API_BASE_URL = import.meta.env.VITE_API_URL;
  ```

#### Code Style

* Use TypeScript strict mode
* Use functional components with hooks
* Prefer `const` over `let`, avoid `var`
* Use destructuring
* Use arrow functions
* Use template literals for strings
* Use optional chaining and nullish coalescing

#### Component Structure

```typescript
import React from 'react';
import { useHabits } from '@/hooks/useHabits';
import type { Habit } from '@/types/habit';

interface HabitCardProps {
  habit: Habit;
  onUpdate?: (habit: Habit) => void;
}

/**
 * Displays a single habit card with completion status
 */
export const HabitCard: React.FC<HabitCardProps> = ({ 
  habit, 
  onUpdate 
}) => {
  const { updateHabit } = useHabits();

  const handleComplete = async () => {
    // Implementation
  };

  return (
    <div className="habit-card">
      {/* JSX */}
    </div>
  );
};
```

#### Hooks

* Use custom hooks for reusable logic
* Prefix hooks with 'use'
* Keep hooks focused and composable

```typescript
export const useHabits = () => {
  const queryClient = useQueryClient();
  
  const { data: habits, isLoading } = useQuery({
    queryKey: ['habits'],
    queryFn: fetchHabits,
  });

  const createMutation = useMutation({
    mutationFn: createHabit,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['habits'] });
    },
  });

  return {
    habits,
    isLoading,
    createHabit: createMutation.mutate,
  };
};
```

#### Styling with Tailwind

* Use Tailwind utility classes
* Extract repeated patterns into components
* Use responsive design utilities
* Follow mobile-first approach

```typescript
<div className="flex flex-col gap-4 p-4 bg-white rounded-lg shadow-md hover:shadow-lg transition-shadow md:flex-row">
```

### Testing Standards

#### Backend Tests

**Unit Tests:**
```csharp
[Fact]
public async Task CreateHabit_WithValidData_ReturnsCreatedHabit()
{
    // Arrange
    var habit = new Habit { Name = "Exercise" };
    
    // Act
    var result = await _habitService.CreateHabitAsync(1, habit);
    
    // Assert
    Assert.NotNull(result);
    Assert.Equal("Exercise", result.Name);
}
```

**Integration Tests:**
```csharp
[Fact]
public async Task CreateHabit_WithValidData_SavesToDatabase()
{
    // Arrange
    var client = _factory.CreateClient();
    var habit = new CreateHabitRequest { Name = "Exercise" };
    
    // Act
    var response = await client.PostAsJsonAsync("/api/habits", habit);
    
    // Assert
    response.EnsureSuccessStatusCode();
}
```

#### Frontend Tests

**Component Tests:**
```typescript
describe('HabitCard', () => {
  it('renders habit name', () => {
    const habit = { id: 1, name: 'Exercise', completed: false };
    render(<HabitCard habit={habit} />);
    
    expect(screen.getByText('Exercise')).toBeInTheDocument();
  });

  it('calls onComplete when checkbox is clicked', async () => {
    const onComplete = vi.fn();
    const habit = { id: 1, name: 'Exercise', completed: false };
    
    render(<HabitCard habit={habit} onComplete={onComplete} />);
    
    await userEvent.click(screen.getByRole('checkbox'));
    
    expect(onComplete).toHaveBeenCalledWith(habit);
  });
});
```

## Git Workflow

### Branch Naming

* `feature/` - New features
* `bugfix/` - Bug fixes
* `hotfix/` - Urgent fixes for production
* `refactor/` - Code refactoring
* `docs/` - Documentation changes
* `test/` - Test additions or modifications

Examples:
* `feature/habit-streak-calculation`
* `bugfix/timer-notification-issue`
* `docs/api-authentication`

### Commit Messages

Follow the format: `<type>(<scope>): <subject>`

**Type:**
* `feat` - New feature
* `fix` - Bug fix
* `docs` - Documentation
* `style` - Formatting, missing semicolons, etc.
* `refactor` - Code restructuring
* `test` - Adding tests
* `chore` - Maintenance tasks

**Examples:**
```
feat(habits): add weekly streak view
fix(auth): resolve token expiration issue
docs(api): update authentication endpoints
test(tasks): add integration tests for task CRUD
refactor(timer): simplify interval calculation
chore(deps): update dependencies
```

### Pull Request Process

1. **Update Documentation** - Ensure README and other docs are updated
2. **Add Tests** - Include tests for new features
3. **Update CHANGELOG** - Add entry for your changes
4. **Pass CI Checks** - All automated checks must pass
5. **Code Review** - At least one approval required
6. **Squash Commits** - Squash commits before merging (if requested)

### Pull Request Template

```markdown
## Description
Brief description of changes

## Type of Change
- [ ] Bug fix
- [ ] New feature
- [ ] Breaking change
- [ ] Documentation update

## Testing
How has this been tested?

## Checklist
- [ ] Code follows style guidelines
- [ ] Self-review completed
- [ ] Code is commented where necessary
- [ ] Documentation updated
- [ ] Tests added/updated
- [ ] All tests passing
- [ ] No linting errors
```

## Code Review Guidelines

### As a Reviewer

* Be respectful and constructive
* Explain why changes are needed
* Ask questions rather than making demands
* Approve when code meets standards
* Review promptly (within 24-48 hours)

### As an Author

* Respond to all comments
* Make requested changes or explain why not
* Don't take criticism personally
* Ask for clarification if needed
* Thank reviewers for their time

## Questions?

Feel free to:
* Open an issue
* Ask in discussions
* Reach out to maintainers

Thank you for contributing! 🎉

