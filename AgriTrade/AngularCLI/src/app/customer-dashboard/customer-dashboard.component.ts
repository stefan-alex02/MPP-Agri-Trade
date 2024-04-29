import { Component, OnInit } from '@angular/core';
import { StockService } from '../../services/stock.service';
import { StockDto } from '../../models/stock-dto';
import {NgForOf} from "@angular/common";

@Component({
  selector: 'app-customer-dashboard',
  templateUrl: './customer-dashboard.component.html',
  standalone: true,
  imports: [
    NgForOf
  ],
  styleUrls: ['./customer-dashboard.component.css']
})
export class CustomerDashboardComponent implements OnInit {
  stocks: StockDto[] = [];

  constructor(private stockService: StockService) { }

  ngOnInit(): void {
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
}
