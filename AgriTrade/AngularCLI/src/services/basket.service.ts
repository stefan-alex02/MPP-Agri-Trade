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
      localStorage.setItem('basket', JSON.stringify(this.basket));    }
  }

  removeProduct(product: Stock): void {
    const index = this.basket.findIndex(item => item.stock.productName === product.productName);
    if (index !== -1) {
      if (this.basket[index].quantity > 1) {
        this.basket[index].quantity -= 1;
      } else {
        this.basket.splice(index, 1);
        localStorage.setItem('basket', JSON.stringify(this.basket));
      }
    }
  }

  getBasket(): {stock: Stock, quantity: number}[] {
    this.basket = JSON.parse(localStorage.getItem('basket') || '[]');
    return this.basket;
  }

  getNumberOfItems() {
    this.basket = JSON.parse(localStorage.getItem('basket') || '[]');
    return this.basket.length;
  }
}
