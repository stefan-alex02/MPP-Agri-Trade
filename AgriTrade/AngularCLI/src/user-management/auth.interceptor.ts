import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpResponse,
  HttpResponseBase
} from '@angular/common/http';
import {EMPTY, Observable, tap} from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import {AuthService} from "../services/auth.service";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private router: Router, private authService: AuthService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Clone the request to add the Authorization header
    const authReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${this.authService.getToken()}`
      }
    });

    return next.handle(authReq).pipe(
      tap((event: HttpEvent<any>) => {
        console.log(event);
        if (event instanceof HttpResponse && event.headers.get('Authorization') !== null) {
          // Handle the HttpResponse, e.g., extract headers
          const newToken = event.headers.get('Authorization');
          if (newToken) {
            console.log('New token', newToken);
            this.authService.saveAuthData(newToken.replace('Bearer ', ''));
          }
        }
      }),
      catchError((error) => {
        // Handle any errors (e.g., 401 Unauthorized)
        if (error.status === 401 || error.status === 403) {
          // Clear authentication data and possibly redirect
          this.authService.clearAuthData();
          this.router.navigate(['/login']);
        }

        // Return the error as an observable
        return EMPTY;
      })
    );


  }
}
