import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { api_config } from '../../../config/api.config';

// Interfaces mapping to your Backend DTOs
export interface LoginResponseDTO {
  token: string;
  refreshToken: string;
  permissions: any[]; // Holds your UserMenuPermissionDTO
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  // Use inject() which is the modern Angular way (Angular 14+)
  private http = inject(HttpClient);
  
  // Reactive state for the JWT token
  private tokenSubject = new BehaviorSubject<string | null>(this.getToken());
  public token$ = this.tokenSubject.asObservable();

  constructor() { }

  /**
   * Register a new student
   * @param data RegisterDTO (Name, Email, Address, Phone, Password)
   */
  register(data: any): Observable<any> {
    return this.http.post(`${api_config.base_url}${api_config.endpoints.auth.register}`, data);
  }

  /**
   * Login user and save tokens on success
   * @param credentials LoginDTO (Email, Password)
   */
  login(credentials: any): Observable<any> {
    return this.http.post<any>(`${api_config.base_url}${api_config.endpoints.auth.login}`, credentials)
      .pipe(
        tap(response => {
          // Because your backend wraps responses in an ApiResponse object:
          // The actual token data is inside response.data
          const authData = response.data as LoginResponseDTO;
          if (authData && authData.token) {
            this.setSession(authData.token, authData.refreshToken, authData.permissions);
          }
        })
      );
  }

  /**
   * Request OTP to be sent to email
   * @param email The user's email
   */
  forgotPassword(email: string): Observable<any> {
    return this.http.post(`${api_config.base_url}${api_config.endpoints.auth.forgot_Password(email)}`, {});
  }

  /**
   * Submit OTP and new password
   * @param email The user's email
   * @param resetData ResetPasswordDTO (Otp, NewPassword)
   */
  resetPassword(email: string, resetData: any): Observable<any> {
    return this.http.post(`${api_config.base_url}${api_config.endpoints.auth.reset_Password(email)}`, resetData);
  }

  /**
   * Refresh an expired token
   * @param tokenData TokenRequestDTO (AccessToken, RefreshToken)
   */
  refreshToken(tokenData: { accessToken: string, refreshToken: string }): Observable<any> {
    return this.http.post<any>(`${api_config.base_url}${api_config.endpoints.auth.refresh}`, tokenData)
      .pipe(
        tap(response => {
           const authData = response.data as LoginResponseDTO;
           if (authData && authData.token) {
               // We only update tokens here, not permissions as they aren't returned in refresh
               sessionStorage.setItem('jwt_token', authData.token);
               sessionStorage.setItem('refresh_token', authData.refreshToken);
               this.tokenSubject.next(authData.token);
           }
        })
      );
  }

  // --- Session Management Helpers ---

  private setSession(token: string, refreshToken: string, permissions: any[]) {
    sessionStorage.setItem('jwt_token', token);
    sessionStorage.setItem('refresh_token', refreshToken);
    sessionStorage.setItem('user_permissions', JSON.stringify(permissions));
    this.tokenSubject.next(token);
  }

  public logout() {
    const token = this.getToken();
    if (token) {
      // Best effort backend logout. We don't await/subscribe strictly because we want to clear local session anyway.
      this.http.post(`${api_config.base_url}${api_config.endpoints.auth.logout}`, { accessToken: token })
        .subscribe({
          next: () => console.log('Logged out successfully from backend'),
          error: (err) => console.warn('Backend logout failed, but clearing local session anyway', err)
        });
    }
    
    sessionStorage.removeItem('jwt_token');
    sessionStorage.removeItem('refresh_token');
    sessionStorage.removeItem('user_permissions');
    this.tokenSubject.next(null);
  }

  public getToken(): string | null {
    return sessionStorage.getItem('jwt_token');
  }

  public isLoggedIn(): boolean {
    return !!this.getToken();
  }
}
