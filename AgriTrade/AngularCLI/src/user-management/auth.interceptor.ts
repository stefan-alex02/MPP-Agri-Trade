import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import {EMPTY, Observable} from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import {AuthService} from "../services/auth.service";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private router: Router, private authService: AuthService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error) => {
        // Check for session expiration (e.g., 401 Unauthorized or 403 Forbidden)
        if (error.status === 401 || error.status === 403) {
          this.authService.clearAuthData();
          console.log(this.router.url);
          // Redirect to the login page
          if (this.router.url !== '') {
            this.router.navigate(['/'])
              .then(r => console.log('Redirected to main page'));
          }
        }

        return EMPTY;
      })
    );
  }
}
