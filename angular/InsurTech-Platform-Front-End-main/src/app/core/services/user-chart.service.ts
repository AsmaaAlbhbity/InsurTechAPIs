import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, forkJoin } from 'rxjs';
import { BASE_URL } from '../base-url';

@Injectable({
  providedIn: 'root'
})
export class UserChartService {
  private apiUrls = [
    `${BASE_URL}/companies`,
     `${BASE_URL}/Account/GetAllUsers`
   ];
 

  constructor(private http: HttpClient) { }
  getCompanies(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrls[0]);
  }
  getUsers(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrls[1]);
  }
}
