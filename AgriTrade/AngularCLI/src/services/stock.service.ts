import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Stock } from '../models/stock';
import config from "../config.json";

@Injectable({
  providedIn: 'root'
})
export class StockService {
  private getStocksUrl = config.baseUrl + '/api/stocks';
  private getStockUrl = config.baseUrl + '/api/stocks';

  constructor(private http: HttpClient) { }

  getStocks(): Observable<Stock[]> {
    return this.http.get<Stock[]>(this.getStocksUrl);
  }

  getStock(id: string): Observable<Stock> {
    return this.http.get<Stock>(`${this.getStockUrl}/${id}`);
  }
}
