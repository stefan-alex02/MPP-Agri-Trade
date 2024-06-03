import { Component, OnInit } from '@angular/core';
import { BasketService } from '../../services/basket.service';
import { StockDto } from '../../models/stock-dto';
import {NgForOf} from "@angular/common";

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  standalone: true,
  imports: [
    NgForOf
  ],
  styleUrls: ['./basket.component.css']
})
export class BasketComponent implements OnInit {
  basketItems: {stock: StockDto, quantity: number}[] = [];
  totalPrice: number = 0;
  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.basketItems = this.basketService.getBasket();
    this.calculateTotalPrice();
  }
  calculateTotalPrice(): void {
    this.totalPrice = this.basketItems.reduce((total, item) => total + (item.stock.price * item.quantity), 0);
  }
  removeProduct(stock: StockDto): void {
    this.basketService.removeProduct(stock);
    this.calculateTotalPrice();
  }
}
