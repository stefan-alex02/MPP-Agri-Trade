import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StockDto } from '../models/stock-dto';
import config from "../config.json";

@Injectable({
  providedIn: 'root'
})
export class StockService {
  private apiUrl = config.baseUrl + '/api/stock'; // replace with your actual API URL

  constructor(private http: HttpClient) { }

  getStocks(): Observable<StockDto[]> {
    return this.http.get<StockDto[]>(this.apiUrl);
  }
}
