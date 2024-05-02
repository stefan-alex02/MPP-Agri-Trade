import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import {Observable, of} from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    console.log('AuthGuard.canActivate');
    return this.checkAuthentication();
  }

  checkAuthentication(): boolean {
    if (this.authService.isLoggedIn()) {
      return true;
    } else {
      console.log('Not logged in. Redirecting to login page.');
      if (this.router.url !== 'login') {
        this.router.navigate(['/login'])
          .then(r => console.log('Redirected to login page.'));
        return false;
      }
      else {
        console.log('Already on home page.');
        return true;
      }
    }
  }
}
