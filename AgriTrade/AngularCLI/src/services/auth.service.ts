import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loginUrl = 'http://localhost'; // replace with your login endpoint

  private _isAuthenticated = false;
  private _userCredentials: any;

  constructor(private http: HttpClient) { }

  get isAuthenticated(): boolean {
    return this._isAuthenticated;
  }

  set isAuthenticated(value: boolean) {
    this._isAuthenticated = value;
  }

  get userCredentials(): any {
    return this._userCredentials;
  }

  set userCredentials(value: any) {
    this._userCredentials = value;
  }

  login(username: string, password: string): Observable<any> {
    console.log('AuthService.login');
    const credentials = { username, password };
    return this.http.post(this.loginUrl, credentials);
  }
}
