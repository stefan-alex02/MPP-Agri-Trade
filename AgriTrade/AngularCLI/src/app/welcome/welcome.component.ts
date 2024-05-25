import { Component } from '@angular/core';
import {MatButton} from "@angular/material/button";
import {NgIf} from "@angular/common";
import {Router} from "@angular/router";
import {AuthService} from "../../services/auth.service";

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

  constructor(private router: Router, protected authService: AuthService) {}

  ngOnInit(): void {
  }

  onLoginClick() {
    this.router
      .navigate(['/login'])
      .then(r => console.log(r));
  }
}
