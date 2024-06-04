import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StockService } from '../../services/stock.service';
import { Stock } from '../../models/stock';
import {MatButton} from "@angular/material/button";
import {BasketService} from "../../services/basket.service";
import {Review} from "../../models/review";
import {ReviewService} from "../../services/review.service";
import {DatePipe, NgForOf, NgIf} from "@angular/common";

@Component({
  selector: 'app-stock-details',
  templateUrl: './stock-details.component.html',
  standalone: true,
  imports: [
    MatButton,
    DatePipe,
    NgForOf,
    NgIf
  ],
  styleUrls: ['./stock-details.component.css']
})
export class StockDetailsComponent implements OnInit {
  stock: Stock = {} as Stock;
  reviews: Review[] = [];

  constructor(
    private route: ActivatedRoute,
    private stockService: StockService,
    private basketService: BasketService,
    private reviewService: ReviewService
  ) { }

  ngOnInit(): void {
    console.log('StockDetailsComponent ngOnInit');
    console.log(this.route.params);
    this.route.params.subscribe(params => {
      const id = params['id'];
      this.stockService.getStock(id).subscribe(stock => {
        this.stock = stock;
        this.reviewService.getReviewsForStock(this.stock).subscribe({
          next: (reviews) => {
            this.reviews = reviews;
          }
        });
      });
    });

  }

  addToCart(stock: Stock) {
    this.basketService.addProduct(stock);
  }
}
