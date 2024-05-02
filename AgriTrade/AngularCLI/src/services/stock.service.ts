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
  private getStockUrl = config.baseUrl + '/api/stock';

  constructor(private http: HttpClient) { }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getStocks(): Observable<StockDto[]> {
    return this.http.get<StockDto[]>(this.getStocksUrl, {
      headers: {
        'Authorization': `Bearer ${this.getToken()}`
      }
    });
  }

  getStock(id: string): Observable<StockDto> {
    return this.http.get<StockDto>(`${this.getStockUrl}/${id}`);
  }
}
