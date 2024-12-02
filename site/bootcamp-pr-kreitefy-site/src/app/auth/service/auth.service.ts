import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserDto } from '../model/user.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  public register(user: UserDto): Observable<UserDto> {
    let urlEndpoint: string = "https://localhost:7026/auth/register";
    return this.http.post<UserDto>(urlEndpoint, user);
  }
}
