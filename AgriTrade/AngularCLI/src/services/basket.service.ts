import { Injectable } from '@angular/core';
import { Stock } from '../models/stock';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  private basket: {stock: Stock, quantity: number}[] = [];

  constructor() { }

  addProduct(product: Stock): void {
    const found = this.basket.find(item => item.stock.productName === product.productName);
    if (found) {
      found.quantity += 1;
    } else {
      this.basket.push({stock: product, quantity: 1});
    }
  }

  removeProduct(product: Stock): void {
    const index = this.basket.findIndex(item => item.stock.productName === product.productName);
    if (index !== -1) {
      if (this.basket[index].quantity > 1) {
        this.basket[index].quantity -= 1;
      } else {
        this.basket.splice(index, 1);
      }
    }
  }

  getBasket(): {stock: Stock, quantity: number}[] {
    return this.basket;
  }

  getNumberOfItems() {
    return this.basket.length;
  }
}
