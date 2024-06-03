import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StockService } from '../../services/stock.service';
import { Stock } from '../../models/stock';
import {MatButton} from "@angular/material/button";
import {BasketService} from "../../services/basket.service";

@Component({
  selector: 'app-stock-details',
  templateUrl: './stock-details.component.html',
  standalone: true,
  imports: [
    MatButton
  ],
  styleUrls: ['./stock-details.component.css']
})
export class StockDetailsComponent implements OnInit {
  stock: Stock = {} as Stock;

  constructor(
    private route: ActivatedRoute,
    private stockService: StockService,
    private basketService: BasketService
  ) { }

  ngOnInit(): void {
    console.log('StockDetailsComponent ngOnInit');
    console.log(this.route.params);
    this.route.params.subscribe(params => {
      const id = params['id'];
      this.stockService.getStock(id).subscribe(stock => {
        this.stock = stock;
      });
    });
  }

  addToCart(stock: Stock) {
    this.basketService.addProduct(stock);
  }
}
