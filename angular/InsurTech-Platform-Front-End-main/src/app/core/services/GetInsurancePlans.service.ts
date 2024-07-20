import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BASE_URL } from '../base-url';

@Injectable({
  providedIn: 'root',
})
export class InsuranceService {
  private baseUrl = `${BASE_URL}/Customers`;
  private feedbackUrl = `${BASE_URL}/Feedback`;

  constructor(private http: HttpClient) {}

  getCustomerRequests(customerId: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetCustomerRequests/${customerId}`);
  }

  postFeedback(feedbackData: any): Observable<any> {
    return this.http.post(this.feedbackUrl, feedbackData);
  }
}
