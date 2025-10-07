# WhatsNext Frontend

Modern React + TypeScript frontend for the WhatsNext Personal Productivity Dashboard.

## 🚀 Tech Stack

- **React 19** - UI library
- **TypeScript 5.9** - Type safety
- **Vite** - Build tool and dev server
- **Tailwind CSS 4** - Utility-first CSS framework
- **ESLint** - Code linting
- **Prettier** - Code formatting

## 📋 Prerequisites

- Node.js 20.x or higher
- npm 10.x or higher

## 🛠️ Getting Started

### Installation

```bash
# Install dependencies
npm install
```

### Development

```bash
# Start dev server (http://localhost:5173)
npm run dev
```

### Building

```bash
# Build for production
npm run build

# Preview production build
npm run preview
```

## 🧹 Code Quality

### Linting

```bash
# Run ESLint
npm run lint

# Fix ESLint issues automatically
npm run lint:fix
```

### Formatting

```bash
# Format all files with Prettier
npm run format

# Check formatting without making changes
npm run format:check
```

### Type Checking

```bash
# Run TypeScript type checker
npm run type-check
```

### Validate Everything

```bash
# Run all checks (type-check, lint, format)
npm run validate
```

## 📁 Project Structure

```
src/
├── components/       # Reusable UI components
│   ├── common/      # Common components (Button, Input, etc.)
│   ├── layout/      # Layout components (Header, Sidebar, etc.)
│   └── features/    # Feature-specific components
├── pages/           # Page components
├── hooks/           # Custom React hooks
├── services/        # API services
├── stores/          # State management
├── types/           # TypeScript type definitions
├── utils/           # Utility functions
├── App.tsx          # Root component
└── main.tsx         # Entry point
```

## 🎨 Styling

### Tailwind CSS

The project uses Tailwind CSS for styling. Custom utilities and components are defined in `src/index.css`.

**Common Classes:**

- `.btn-primary` - Primary button style
- `.btn-secondary` - Secondary button style
- `.card` - Card container style

### Path Aliases

TypeScript path aliases are configured for cleaner imports:

```typescript
// Instead of: import Button from '../../../components/common/Button'
import Button from '@/components/common/Button';

// Available aliases:
// @/*              -> ./src/*
// @/components/*   -> ./src/components/*
// @/pages/*        -> ./src/pages/*
// @/hooks/*        -> ./src/hooks/*
// @/services/*     -> ./src/services/*
// @/stores/*       -> ./src/stores/*
// @/types/*        -> ./src/types/*
// @/utils/*        -> ./src/utils/*
```

## 🔧 Configuration Files

### ESLint (`.eslintrc.json`)

Configured with:

- TypeScript support
- React + React Hooks rules
- JSX accessibility (a11y) rules
- Prettier integration
- Custom rules for code quality

### Prettier (`.prettierrc`)

Configured with:

- Single quotes
- 2-space indentation
- 100 character line width
- Trailing commas (ES5)
- Semicolons enabled

### TypeScript (`tsconfig.app.json`)

Configured with:

- Strict mode enabled
- Path aliases
- Modern ES2022 target
- React JSX support
- Comprehensive type checking

## 🔌 API Integration

The dev server is configured to proxy API requests to the backend:

```typescript
// Automatically proxied to https://localhost:7001
fetch('/api/habits');
```

Configure the backend URL in `.env.local` (create from `.env.example`):

```env
VITE_API_URL=https://localhost:7001
```

## 📝 Code Style Guidelines

### Naming Conventions

- **Components**: PascalCase (`Button.tsx`, `UserProfile.tsx`)
- **Files**: PascalCase for components, camelCase for utilities
- **Functions**: camelCase (`handleClick`, `fetchUserData`)
- **Constants**: UPPER_SNAKE_CASE (`API_BASE_URL`)
- **Types/Interfaces**: PascalCase (`User`, `HabitData`)

### Component Structure

```typescript
import React from 'react';

interface ButtonProps {
  label: string;
  onClick: () => void;
  variant?: 'primary' | 'secondary';
}

export const Button: React.FC<ButtonProps> = ({
  label,
  onClick,
  variant = 'primary'
}) => {
  return (
    <button
      onClick={onClick}
      className={variant === 'primary' ? 'btn-primary' : 'btn-secondary'}
    >
      {label}
    </button>
  );
};
```

### TypeScript Best Practices

- Always define types for props
- Use `interface` for public APIs, `type` for unions/intersections
- Avoid `any` - use `unknown` if type is truly unknown
- Leverage TypeScript's strict mode
- Use enums sparingly, prefer union types

## 🧪 Testing (Coming Soon)

```bash
# Run tests
npm run test

# Run tests with coverage
npm run test:coverage

# Run tests in watch mode
npm run test:watch
```

## 🚢 Deployment

The production build is optimized and ready for deployment:

```bash
npm run build
```

Output will be in the `dist/` directory.

### Environment Variables

Required environment variables for production:

```env
VITE_API_URL=https://api.yourproduction.com
VITE_ENV=production
```

## 🔍 Troubleshooting

### ESLint Errors

If you see ESLint errors:

1. Make sure all dependencies are installed: `npm install`
2. Check that your code follows the style guide
3. Run `npm run lint:fix` to auto-fix issues

### Type Errors

If TypeScript shows errors:

1. Run `npm run type-check` to see all errors
2. Ensure all imports have proper types
3. Check `tsconfig.app.json` for configuration

### Tailwind Not Working

If Tailwind classes aren't applying:

1. Ensure `@tailwind` directives are in `src/index.css`
2. Check `tailwind.config.js` includes your files
3. Restart the dev server

## 📚 Resources

- [React Documentation](https://react.dev/)
- [TypeScript Handbook](https://www.typescriptlang.org/docs/)
- [Vite Guide](https://vitejs.dev/guide/)
- [Tailwind CSS Docs](https://tailwindcss.com/docs)
- [ESLint Rules](https://eslint.org/docs/rules/)
- [Prettier Options](https://prettier.io/docs/en/options.html)

## 🤝 Contributing

See [CONTRIBUTING.md](../CONTRIBUTING.md) in the root directory for contribution guidelines.

## 📄 License

MIT License - see [LICENSE](../LICENSE) for details
