import { Component } from '@angular/core';
import {Router} from "@angular/router";
import {AuthService} from "../../services/auth.service";
import {UserType} from "../../models/user-type";
import {RoleGuard} from "../../guards/role.guard";
import {BasketService} from "../../services/basket.service";


@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.css'
})
export class MainLayoutComponent {
  title = 'AgriTrade';

  constructor(private router: Router, protected authService: AuthService, protected basketService: BasketService,
              private authGuard: RoleGuard) {}

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
    this.router.navigate(['/customer-dashboard']);
   }

  refreshProducerDashboard() {
    this.router.navigate(['/producer-dashboard']);
  }

  pressBasket() {
    this.router.navigate(['/basket']);
  }
}
