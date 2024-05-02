import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpResponse} from "@angular/common/http";
import {Observable, tap} from "rxjs";
import config from '../config.json';
import {jwtDecode} from "jwt-decode";
import moment from 'moment';
import {UserType} from "../utils/user-type";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loginUrl = config.baseUrl + '/api/user/login';
  private logoutUrl = config.baseUrl + '/api/user/logout';

  constructor(private http: HttpClient) {}

  saveAuthData(token: string): void {
    const decodedToken: any = jwtDecode(token);

    console.log(decodedToken);

    localStorage.setItem('token', token);
    localStorage.setItem('id', decodedToken.user_id);
    localStorage.setItem('username', decodedToken.username);
    localStorage.setItem('user_role', decodedToken.user_role);
    localStorage.setItem('expires_at', decodedToken.exp);
  }

  clearAuthData(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('id');
    localStorage.removeItem('username');
    localStorage.removeItem('user_role');
    localStorage.removeItem('expires_at');
  }

  public getToken() {
    return localStorage.getItem('token');
  }

  getExpiration() {
    const expiration = localStorage.getItem("expires_at");
    const expiresAt = JSON.parse(expiration!);

    return moment.unix(expiresAt);
  }

  public isLoggedIn() {
    if (!moment().isBefore(this.getExpiration())) {
      this.clearAuthData();
      return false;
    }
    return true;
  }

  isLoggedOut() {
    return !this.isLoggedIn();
  }

  getUserRole() {
    if (!this.isLoggedIn()) {
      return UserType.None;
    }
    return Number(localStorage.getItem('user_role'));
  }

  isConsumer() {
    return this.getUserRole() === UserType.Consumer;
  }

  isProducer() {
    return this.getUserRole() === UserType.Producer;
  }

  login(username: string, password: string): Observable<any> {
    const loginRequest = { username, password };

    return this.http.post<any>(this.loginUrl, loginRequest, { withCredentials: true }).pipe(
      tap((response) => {
        this.saveAuthData(response.token); // Save the JWT token and decode it
      })
    );
  }

  logout(): Observable<any> {
    return this.http.post<any>(this.logoutUrl, {}, { withCredentials: true }).pipe(
      tap(() => {
        this.clearAuthData();
      })
    );
  }
}
