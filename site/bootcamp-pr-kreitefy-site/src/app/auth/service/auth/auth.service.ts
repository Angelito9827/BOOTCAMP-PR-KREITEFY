import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { UserDto } from '../../model/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedInSubject = new BehaviorSubject<boolean>(this.isAuthenticated());
  private userNameSubject = new BehaviorSubject<string>(this.getDecodedUserName() || '');



  constructor(private http: HttpClient) { }

  public register(user: UserDto): Observable<UserDto> {
    let urlEndpoint: string = "https://localhost:7026/auth/register";
    return this.http.post<UserDto>(urlEndpoint, user).pipe(
      tap((response: any) => {
        const token = response.token;
        this.saveToken(token);
      })
    );
  }

  public login(credentials: any): Observable<any> {
    let urlEndpoint: string = "https://localhost:7026/auth/login";
    return this.http.post(urlEndpoint, credentials).pipe(
      tap((response: any) => {
        const token = response.token;
        this.saveToken(token);
      })
    );

  }

  saveToken(token: string): void {
    if (this.isBrowser()) {
      localStorage.setItem('authToken', token);
      const userName = this.getDecodedUserName();
      localStorage.setItem('username', userName || '');
      this.loggedInSubject.next(true);
      this.userNameSubject.next(userName || '');
    }
  }

  getToken(): string | null {
    return this.isBrowser() ? localStorage.getItem('authToken') : null;
  }

  private getDecodedUserName(): string | null {
    const token = this.getToken();
    if (token) {
      try {
        const payload = token.split('.')[1];
        const decodedPayload = JSON.parse(atob(payload));
        return decodedPayload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"] || null; // Acceder al claim correcto
      } catch (e) {
        console.error('Error decoding token:', e);
        return null;
      }
    }
    return null;
  }

  isAuthenticated(): boolean {
    return this.getToken() !== null;
  }

  logout(): void {
    if (this.isBrowser()) {
      localStorage.removeItem('authToken');
      this.loggedInSubject.next(false);
      this.userNameSubject.next('');
    }
  }

  getLoggedInStatus(): Observable<boolean> {
    return this.loggedInSubject.asObservable();
  }

  getUserNameObservable(): Observable<string> {
    return this.userNameSubject.asObservable();
  }

  private isBrowser(): boolean {
    return typeof window !== 'undefined' && typeof window.localStorage !== 'undefined';
  }
}

