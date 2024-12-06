import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { jwtDecode } from 'jwt-decode';
import { UserRegister } from '../../model/user-register.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
 
  private readonly tokenKey = 'authToken';
  private readonly userKey = 'username';
  private readonly userId = 'userid';
  private authStatus$ = new BehaviorSubject<boolean>(this.hasToken());
  private userName$ = new BehaviorSubject<string>(this.getStoredUserName());
  private userId$ = new BehaviorSubject<string>(this.getStoredUserId());

  constructor(private http: HttpClient) {}

  register(userData: UserRegister){
    const endpoint = 'http://localhost:5272/auth/register';
    return this.http.post(endpoint, userData);
  }

  login(credentials: any): Observable<any> {
    const endpoint = 'http://localhost:5272/auth/login';
    return this.http.post<any>(endpoint, credentials).pipe(
      tap((res: any) => {
        if (res.token) {
          this.saveToken(res.token);
        }
      })
    );
  }

  logout(): void {
    if (this.isBrowser()) {
      localStorage.removeItem(this.tokenKey);
      localStorage.removeItem(this.userKey);
      localStorage.removeItem(this.userId);
    }
    this.authStatus$.next(false);
    this.userName$.next('');
    this.userId$.next('');
  }

  getAuthStatus(): Observable<boolean> {
    return this.authStatus$.asObservable();
  }

  getUserNameObservable(): Observable<string> {
    return this.userName$.asObservable();
  }

  getUserIdObservable(): Observable<string> {
    return this.userId$.asObservable();
  }

  private saveToken(token: string): void {
    if (this.isBrowser()) {
      localStorage.setItem(this.tokenKey, token);

      const decodedToken = this.decodeToken(token);
      const userName = decodedToken?.['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] || '';
      const userId = decodedToken?.['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] || '';

      localStorage.setItem(this.userKey, userName);
      this.userName$.next(userName);

      localStorage.setItem(this.userId, userId);
      this.userId$.next(userId);
    }
    this.authStatus$.next(true);
  }

  private decodeToken(token: string): any {
    try {
      return jwtDecode(token);
    } catch (error) {
      console.error('Error decoding token:', error);
      return null;
    }
  }

  private hasToken(): boolean {
    return this.isBrowser() && !!localStorage.getItem(this.tokenKey);
  }

  private getStoredUserName(): string {
    return this.isBrowser() ? localStorage.getItem(this.userKey) || '' : '';
  }

  getStoredUserId(): string {
    return this.isBrowser() ? localStorage.getItem(this.userId) || '' : '';
  }

  private isBrowser(): boolean {
    return typeof window !== 'undefined';
  }
}
