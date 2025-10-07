import { api } from './api';
import type { AuthResponse, LoginRequest, RegisterRequest } from '../types/auth';

export const authService = {
  async login(credentials: LoginRequest): Promise<AuthResponse> {
    const response = await api.post<AuthResponse>('/authentication/login', credentials);
    return response.data;
  },

  async register(data: RegisterRequest): Promise<AuthResponse> {
    const response = await api.post<AuthResponse>('/authentication/register', data);
    return response.data;
  },

  async loginWithGoogle(accessToken: string): Promise<AuthResponse> {
    const response = await api.post<AuthResponse>('/authentication/google', {
      accessToken,
    });
    return response.data;
  },

  saveToken(token: string): void {
    localStorage.setItem('token', token);
  },

  getToken(): string | null {
    return localStorage.getItem('token');
  },

  removeToken(): void {
    localStorage.removeItem('token');
  },

  saveUser(user: AuthResponse): void {
    localStorage.setItem('user', JSON.stringify(user));
  },

  getUser(): AuthResponse | null {
    const userStr = localStorage.getItem('user');
    if (!userStr) return null;
    try {
      return JSON.parse(userStr);
    } catch {
      return null;
    }
  },

  removeUser(): void {
    localStorage.removeItem('user');
  },

  clearAuth(): void {
    this.removeToken();
    this.removeUser();
  },
};

