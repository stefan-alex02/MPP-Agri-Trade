import { Component } from '@angular/core';
import {Router} from "@angular/router";
import {AuthService} from "../../services/auth.service";

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.css'
})
export class MainLayoutComponent {
  title = 'AgriTrade';

  constructor(private router: Router, protected authService: AuthService) {}

  ngOnInit() {
    if (!this.authService.isAuthenticated()) {
      console.log('User is logged in');
      return;
    }

    this.authService.checkSession().subscribe({
      next: () => {
        console.log('Session is still active');
      },
      error: (error) => {
        if (error.status === 450) {
          console.log('No session found');
        } else {
          console.error('Error checking session:', error);
        }
      }
    });
  }

  pressLogin() {
    this.router
      .navigate(['/login'])
      .then(r => console.log(r));
  }

  pressLogout() {
    this.authService.logout().subscribe({
      next: () => {
        console.log('Logout successful');
        // Navigate to the main page
        this.router.navigate(['/'])
          .then(r => console.log('Navigated to main page:', r));
      },
      error: (error) => {
        console.error('Logout failed:', error);
      }
    });
  }

  pressProfile() {

  }
}
