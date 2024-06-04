import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Review } from '../models/review';
import { Stock } from '../models/stock';
import config from "../config.json";

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  private getReviewsUrl = config.baseUrl + '/api/reviews'

  constructor(private http: HttpClient) { }

  getReviewsForStock(stock: Stock): Observable<Review[]> {
    return this.http.get<Review[]>(this.getReviewsUrl).pipe(
      map(reviews => reviews.filter(review =>
        review.toUsername === stock.producerUsername
      ))
    );
  }
}
