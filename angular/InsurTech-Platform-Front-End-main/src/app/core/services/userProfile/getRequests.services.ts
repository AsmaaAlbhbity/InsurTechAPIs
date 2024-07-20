import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BASE_URL } from '../../base-url';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class getRequsestsService {

  constructor(private http: HttpClient) { }
  getUserRequestsWithPendingStatus(): Observable<any> {
      return this.http.get(`${BASE_URL}/Customers/getUserRequestswithPendingStatus`);
    }
  
    getRequestsWithPaidTrue(): Observable<any> {
      return this.http.get(`${BASE_URL}/Customers/getRequestsWtihPaidTrue`);
    }
  
    getRequestsWithApprovedStatusButNotPaid(): Observable<any> {
      return this.http.get(`${BASE_URL}/Customers/getRequestsWtihApprovedStatusbutNotPaid`);
    }
  
    getRequestsWithRejectedStatus(): Observable<any> {
      return this.http.get(`${BASE_URL}/Customers/getRequestsWtihRejectedStatus`);
    }
    changePaidToFalse(insurancePlanId: number): Observable<any> {
      return this.http.put(`${BASE_URL}/Customers/CancelPaid?insurancePlanId=${insurancePlanId}`, {});
    }
 
}
