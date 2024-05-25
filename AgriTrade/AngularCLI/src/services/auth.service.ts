import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpResponse} from "@angular/common/http";
import {Observable, tap} from "rxjs";
import config from '../config.json';
import {jwtDecode} from "jwt-decode";
import moment from 'moment';
import {UserType} from "../models/user-type";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loginUrl = config.baseUrl + '/api/users/login';
  private logoutUrl = config.baseUrl + '/api/users/logout';

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<any> {

    return this.http.post<any>(this.loginUrl, { username, password },
      { withCredentials: true }).pipe(
      tap((response) => {
        this.saveJwtToken(response.token);
      })
    );
  }

  logout(): Observable<any> {
    return this.http.post<any>(this.logoutUrl, {}, { withCredentials: true }).pipe(
      tap(() => {
        this.clearJwtToken();
      })
    );
  }

  saveJwtToken(token: string): void {
    const decodedToken: any = jwtDecode(token);
    console.log('Decoded token: ', decodedToken);

    localStorage.setItem('token', token);
    localStorage.setItem('id', decodedToken.user_id);
    localStorage.setItem('username', decodedToken.username);
    localStorage.setItem('name', decodedToken.name);
    localStorage.setItem('user_type', decodedToken.user_type);
    localStorage.setItem('expires_at', decodedToken.exp);

    console.log('Token saved');
  }

  clearJwtToken(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('id');
    localStorage.removeItem('username');
    localStorage.removeItem('name');
    localStorage.removeItem('user_type');
    localStorage.removeItem('expires_at');

    console.log('Token removed');
  }

  public getToken() {
    return localStorage.getItem('token');
  }

  public isLoggedIn(): boolean {
    if (this.getToken() === null) {
      return false;
    }
    if (this.isTokenExpired()) {
      this.clearJwtToken();
      return false;
    }
    return true;
  }

  isTokenExpired(): boolean {
    const expiration = localStorage.getItem("expires_at");
    const expiresAt = JSON.parse(expiration!);
    console.log('Expires at: ', moment.unix(expiresAt).format('YYYY-MM-DD HH:mm:ss'));
    return moment().isAfter(moment.unix(expiresAt));
  }

  public getUserId(): number {
    return Number(localStorage.getItem('id') ?? -1);
  }

  public getUserType(): UserType {
    return Number(localStorage.getItem('user_type'));
  }

  public getName(): string {
    return localStorage.getItem('name') ?? '';
  }
}
