import { Component } from '@angular/core';
import {MatButton} from "@angular/material/button";
import {AuthService} from "../../services/auth.service";
import {NgIf} from "@angular/common";
import {Router} from "@angular/router";

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  standalone: true,
  imports: [
    MatButton,
    NgIf
  ],
  styleUrls: ['./welcome.component.css']
})
export class WelcomeComponent {
  title = 'AgriTrade';

  constructor(protected authService: AuthService, private router: Router) {}

  onLoginClick() {
    this.router
      .navigate(['/login'])
      .then(r => console.log(r));
  }
}
