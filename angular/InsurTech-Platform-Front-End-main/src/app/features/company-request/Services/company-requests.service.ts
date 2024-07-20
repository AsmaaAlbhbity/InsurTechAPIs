import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CompanyRequests } from '../Models/company-requests';
import { BASE_URL } from '../../../core/base-url';

@Injectable({
  providedIn: 'root'
})
export class CompanyRequestsService {
  private BaseUrl = `${BASE_URL}/companies/`;

  constructor(private http: HttpClient) {}

  GetAllRequestsByCompanyId(companyId: string): Observable<CompanyRequests[]> {
    return this.http.get<CompanyRequests[]>(`${this.BaseUrl}requests/${companyId}`);
  }
  UpdateAndNotify(RequestId: string, request: CompanyRequests): Observable<any> {
    const url = `${this.BaseUrl}ChangeStatus/${RequestId}`;
    return this.http.put<any>(url, request);
    
  }
  
  
}
