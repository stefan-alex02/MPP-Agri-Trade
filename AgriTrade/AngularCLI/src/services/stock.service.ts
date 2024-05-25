import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StockDto } from '../models/stock-dto';
import config from "../config.json";

@Injectable({
  providedIn: 'root'
})
export class StockService {
  private getStocksUrl = config.baseUrl + '/api/stocks';
  private getStockUrl = config.baseUrl + '/api/stocks';

  constructor(private http: HttpClient) { }

  getStocks(): Observable<StockDto[]> {
    return this.http.get<StockDto[]>(this.getStocksUrl);
  }

  getStock(id: string): Observable<StockDto> {
    return this.http.get<StockDto>(`${this.getStockUrl}/${id}`);
  }
}
