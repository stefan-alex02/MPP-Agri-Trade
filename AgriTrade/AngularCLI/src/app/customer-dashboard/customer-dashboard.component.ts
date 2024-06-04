import { Component, OnInit } from '@angular/core';
import { StockService } from '../../services/stock.service';
import { Stock } from '../../models/stock';
import {NgForOf, NgOptimizedImage} from "@angular/common";
import {MatButton} from "@angular/material/button";
import {Router} from "@angular/router";

@Component({
  selector: 'app-customer-dashboard',
  templateUrl: './customer-dashboard.component.html',
  standalone: true,
  imports: [
    NgForOf,
    NgOptimizedImage,
    MatButton
  ],
  styleUrls: ['./customer-dashboard.component.css']
})
export class CustomerDashboardComponent implements OnInit {
  stocks: Stock[] = [];

  constructor(private stockService: StockService, private router: Router) { }

  ngOnInit(): void {
    console.log('CustomerDashboardComponent.ngOnInit');
    this.loadStocks();
  }

  loadStocks(): void {
    this.stockService.getStocks().subscribe({
      next: (stocks) => {
        this.stocks = stocks;
      },
      error: (error) => {
        console.error('Error loading stocks:', error);
      }
    });
  }

  navigateToDetails(stock: Stock) {
    console.log('navigating to stock details');
    this.router.navigate(['/stocks', stock.stockId])
      .then(r => console.log('navigated to stock details'));
  }

  addToCart(stock: Stock) {

  }

}
