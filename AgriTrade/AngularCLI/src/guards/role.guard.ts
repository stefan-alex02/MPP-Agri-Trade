import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import {Observable, of} from 'rxjs';
import { AuthService } from '../services/auth.service';
import {UserType} from "../models/user-type";
import { Location } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {

  constructor(private authService: AuthService,
              private router: Router,
              private location: Location) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

    if (!this.authService.isLoggedIn()) {
      console.log('Role guard: User is not logged in');
      this.router.navigate(['/login'])
        .then(r => console.log('Redirected to login page:', r));
      return false;
    }

    const requiredRole = route.data['role'] as UserType;
    if (this.authService.getUserType() === requiredRole) {
      console.log('Role guard: User has required role: ', requiredRole);
      return true;
    }

    console.log('Role guard: User does not have required role: ', requiredRole);
    this.location.back();
    return false;
  }
}
