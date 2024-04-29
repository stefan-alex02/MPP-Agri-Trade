import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpResponse} from "@angular/common/http";
import {Observable, tap} from "rxjs";
import config from '../config.json';
import {UserType} from "../utils/user-type";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loginUrl = config.baseUrl + '/api/user/login';
  private logoutUrl = config.baseUrl + '/api/user/logout';
  private sessionUrl = config.baseUrl + '/api/user/session';

  constructor(private http: HttpClient) { }

  saveAuthData(userType: number): void {
    localStorage.setItem('userType', userType.toString());
    localStorage.setItem('isAuthenticated', 'true');
  }

  clearAuthData(): void {
    localStorage.removeItem('userType');
    localStorage.removeItem('isAuthenticated');
  }

  isAuthenticated(): boolean {
    return localStorage.getItem('isAuthenticated') === 'true';
  }

  getUserType(): number {
    if (!this.isAuthenticated()) {
      return -1;
    }
    return parseInt(localStorage.getItem('userType')!);
  }

  isConsumer(): boolean {
    return this.getUserType() === UserType.Consumer;
  }

  isProducer(): boolean {
    return this.getUserType() === UserType.Producer;
  }

  login(username: string, password: string): Observable<any> {
    console.log('Sending login request to:', this.loginUrl);

    const loginRequest = { username, password };

    return this.http.post<any>(this.loginUrl, loginRequest, { withCredentials: true }).pipe(
      tap(response => {
        console.log('Login response:', response)
        this.saveAuthData(response.userType);
      })
    );
  }

  logout(): Observable<any> {
    return this.http.post<any>(this.logoutUrl, {}, { withCredentials: true }).pipe(
      tap(() => {
        // Clear the user type and authentication status from the localStorage
        this.clearAuthData();
      })
    );
  }

  checkSession(): Observable<any> {
    return this.http.get<any>(this.sessionUrl, { withCredentials: true });
  }
}
