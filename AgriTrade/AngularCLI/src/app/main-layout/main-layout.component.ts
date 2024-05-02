import { Component } from '@angular/core';
import {Router} from "@angular/router";
import {AuthService} from "../../services/auth.service";
import {UserType} from "../../utils/user-type";
import {AuthGuard} from "../../user-management/auth.guard";

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.css'
})
export class MainLayoutComponent {
  title = 'AgriTrade';

  constructor(private router: Router, protected authService: AuthService,
              private authGuard: AuthGuard) {}

  ngOnInit() {
    console.log('MainLayoutComponent.ngOnInit');
    // if (!this.authService.isLoggedIn() && this.router.url !== '') {
    //   this.router.navigate(['/'])
    //     .then(r => console.log('Navigated to main page:', r));
    //   return;
    // }
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

  protected readonly UserType = UserType;

  refreshCustomerDashboard() {
    if (!this.authGuard.checkAuthentication()) {
      return;
    }
    this.router.navigate(['/customer-dashboard']);
   }

  refreshProducerDashboard() {
    if (!this.authGuard.checkAuthentication()) {
      return;
    }
    this.router.navigate(['/producer-dashboard']);
  }
}
