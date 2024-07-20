import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CompaniesHomePage } from '../../models/Home_Page/CompaniesHomePage';
import { Observable } from 'rxjs';
import { BASE_URL } from '../../base-url';

@Injectable({
  providedIn: 'root'
})
export class GetCompaniesService {

  private apiUrl = `${BASE_URL}/companies/status/approved`;

  constructor(private http: HttpClient) { }

  getCompanies(): Observable<CompaniesHomePage[]> {
    return this.http.get<CompaniesHomePage[]>(this.apiUrl);
  }
}
