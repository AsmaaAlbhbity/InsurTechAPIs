import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { InsuranceReview } from '../types/IAllFeedBackes';
import { BASE_URL } from '../base-url';

@Injectable({
  providedIn: 'root',
})
export class ComparingInsuranceService {
  constructor(private http: HttpClient) {}
  BaseUrl = `${BASE_URL}/Feedback`;
  // get all feedbacks
  getAllFeedbacks() {
    return this.http.get<InsuranceReview[]>(this.BaseUrl);
  }
}
