import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { map, switchMap } from 'rxjs';
import { BASE_URL } from '../base-url';

@Injectable({
  providedIn: 'root',
})
export class CategoriesService {
  constructor(private http: HttpClient) {}

  getCategories(url: string): Observable<any> {
    return this.http.get<any>(url, { headers: { accept: '*/*' } });
  }

  getCompanyDetails(url: string): Observable<any> {
    return this.http.get<any>(url, { headers: { accept: '*/*' } });
  }

  getInsurancePlans(companyId: string): Observable<any> {
    const plansUrl = `${BASE_URL}/InsurancePlan/SoldInsuranceByCompanyId/${companyId}`;
    return this.http.get<any>(plansUrl, { headers: { accept: '*/*' } });
  }

  getCompanyIdAndInsurancePlans(companyUrl: string): Observable<any> {
    return this.getCompanyDetails(companyUrl).pipe(
      map((response) => response.companyId),
      switchMap((companyId) => this.getInsurancePlans(companyId))
    );
  }
  getInsuranceUsers(insuranceId: string): Observable<any> {
    
    const usersUrl = `${BASE_URL}/InsurancePlan/SoldInsuranceByCompanyId/1`;
    return this.http.get<any>(usersUrl, { headers: { accept: '*/*' } });
  }


}

