import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StockService } from '../../services/stock.service';
import { StockDto } from '../../models/stock-dto';

@Component({
  selector: 'app-stock-details',
  templateUrl: './stock-details.component.html',
  standalone: true,
  styleUrls: ['./stock-details.component.css']
})
export class StockDetailsComponent implements OnInit {
  stock: StockDto = {} as StockDto;

  constructor(
    private route: ActivatedRoute,
    private stockService: StockService
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
}
