import { Component, OnInit } from '@angular/core';
import { BasketService } from '../../services/basket.service';
import { Stock } from '../../models/stock';
import {NgForOf} from "@angular/common";
import {MatButton} from "@angular/material/button";

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  standalone: true,
  imports: [
    NgForOf,
    MatButton
  ],
  styleUrls: ['./basket.component.css']
})
export class BasketComponent implements OnInit {
  basketItems: {stock: Stock, quantity: number}[] = [];
  totalPrice: number = 0;
  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.basketItems = this.basketService.getBasket();
    this.calculateTotalPrice();
  }
  calculateTotalPrice(): void {
    this.totalPrice = this.basketItems.reduce((total, item) => total + (item.stock.price * item.quantity), 0);
  }
  removeProduct(stock: Stock): void {
    this.basketService.removeProduct(stock);
    this.basketItems = this.basketService.getBasket();
    this.calculateTotalPrice();
  }
}
