import { Component } from '@angular/core';
import {MatButton} from "@angular/material/button";
import {MatFormField, MatInput, MatInputModule} from "@angular/material/input";
import {AuthService} from "../../../services/auth.service";
import {FormsModule} from "@angular/forms";
import {Router, RouterLink} from "@angular/router";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    MatButton,
    MatInput,
    MatInputModule,
    MatFormField,
    FormsModule,
    RouterLink,
    NgIf
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  onLoginClick() {
    this.authService.login(this.username, this.password).subscribe({
      next: () => {
        console.log("User is logged in");
        this.router.navigate(['/'])
          .then(r => console.log('Navigated to /'));
      },
      error: (error) => {
        console.error('Login failed:', error);
        this.errorMessage = 'Login failed. ' + error.error;
      }
    });
  }
}
