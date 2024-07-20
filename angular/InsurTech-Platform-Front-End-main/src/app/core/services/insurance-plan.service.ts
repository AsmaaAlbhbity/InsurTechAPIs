// insurance-plan.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiResponse, InsurancePlan } from '../types/UserInsurancePlans';
import { BASE_URL } from '../base-url';

@Injectable({
  providedIn: 'root',
})
export class InsurancePlanService {
  private apiUrl =
    `${BASE_URL}/InsurancePlan/InsurancePlansByCustomerId`;

  constructor(private http: HttpClient) {}

  getInsurancePlansByCustomerId(customerId: string): Observable<ApiResponse> {
    return this.http.get<ApiResponse>(`${this.apiUrl}/${customerId}`).pipe(
      map((response) => ({
        healthInsurancePlans: response.healthInsurancePlans.map((plan) => ({
          id: plan.id,
          company: plan.company,
          category: plan.category,
          level: plan.level,
        })),
        motorInsurancePlans: response.motorInsurancePlans.map((plan) => ({
          id: plan.id,
          company: plan.company,
          category: plan.category,
          level: plan.level,
        })),
        homeInsurancePlans: response.homeInsurancePlans.map((plan) => ({
          id: plan.id,
          company: plan.company,
          category: plan.category,
          level: plan.level,
        })),
      }))
    );
  }
}
