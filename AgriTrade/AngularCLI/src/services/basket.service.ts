import { Injectable } from '@angular/core';
import { StockDto } from '../models/stock-dto';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  private basket: {stock: StockDto, quantity: number}[] = [];

  constructor() { }

  addProduct(product: StockDto): void {
    const found = this.basket.find(item => item.stock.productName === product.productName);
    if (found) {
      found.quantity += 1;
    } else {
      this.basket.push({stock: product, quantity: 1});
    }
  }

  removeProduct(product: StockDto): void {
    const index = this.basket.findIndex(item => item.stock.productName === product.productName);
    if (index !== -1) {
      if (this.basket[index].quantity > 1) {
        this.basket[index].quantity -= 1;
      } else {
        this.basket.splice(index, 1);
      }
    }
  }

  getBasket(): {stock: StockDto, quantity: number}[] {
    return this.basket;
  }

  getNumberOfItems() {
    return this.basket.length;
  }
}
