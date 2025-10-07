export interface User {
  userId: number;
  username: string;
  email: string;
  roles: string[];
}

export interface AuthResponse {
  userId: number;
  username: string;
  email: string;
  token: string;
  refreshToken: string;
  expiresAt: string;
  roles: string[];
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  username: string;
  email: string;
  password: string;
  confirmPassword: string;
}

export interface AuthContextType {
  user: User | null;
  token: string | null;
  isAuthenticated: boolean;
  isLoading: boolean;
  login: (email: string, password: string) => Promise<void>;
  register: (data: RegisterRequest) => Promise<void>;
  loginWithGoogle: () => Promise<void>;
  logout: () => void;
}

