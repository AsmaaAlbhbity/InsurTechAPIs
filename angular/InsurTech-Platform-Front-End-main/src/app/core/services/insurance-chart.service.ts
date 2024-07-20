import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, forkJoin } from 'rxjs';
import { BASE_URL } from '../base-url';


@Injectable({
  providedIn: 'root'
})
export class InsuranceChartService {
  private apiUrls = [
   `${BASE_URL}/HealthInsurance/GetHealthInsurance`,
    `${BASE_URL}/HomeInsurance/GetHomeInsurance`,
    `${BASE_URL}/MotorInsurance/GetMotorInsurance`
  ];

  constructor(private http: HttpClient) {}

  getAllData(): Observable<any[]> {
    const requests = this.apiUrls.map(url => this.http.get<any[]>(url));
    return forkJoin(requests);
  }
  getHealthInsurance(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrls[0]);
  }
  getHomeInsurance(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrls[1]);
  }
  getMotorInsurance(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrls[2]);
  }
  
}
