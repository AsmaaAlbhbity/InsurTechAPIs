import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ListInsurancePlan } from '../../Model/company/ListInsurancePlan';
import { BehaviorSubject, Observable } from 'rxjs';
import { CompanyUsers } from '../../Model/company/CompanyUser';
import { HealthinsuranceService } from '../ManageHealthServices/healthinsurance.service';
import { HomeinsuranceService } from '../ManageHomeServices/homeinsurance.service';
import { MotorinsuranceService } from '../ManageMotorServices/motorinsurance.service';
import { BASE_URL } from '../../../../core/base-url';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  private baseUrl: string = `${BASE_URL}/`;
  public companId:string=JSON.parse(localStorage.getItem('userData') || "{}").id;
  private insurancePlansSubject = new BehaviorSubject<ListInsurancePlan | null>(null);
  insurancePlans$ = this.insurancePlansSubject.asObservable();

  constructor(private httpClient: HttpClient,
     private healthinsuranceService: HealthinsuranceService,
     private homeinsuranceServices:HomeinsuranceService,
     private MotornsuranceServies:MotorinsuranceService
    ) {
    this.healthinsuranceService.healthInsuranceChanges$.subscribe(()=>{this.GetAll(this.companId);});
    this.MotornsuranceServies.motorinsurancechanges$.subscribe(()=>{this.GetAll(this.companId);});
    this.homeinsuranceServices.homeinsurancechanges$.subscribe(()=>{this.GetAll(this.companId);});
    
  }

  GetAll(Id: string): void {
    this.httpClient.get<ListInsurancePlan>(this.baseUrl + "InsurancePlan/InsurancePlansByCompanyId/" + Id)
      .subscribe(data => {
        this.insurancePlansSubject.next(data);
      });
  }

  Delete(Id: number): Observable<any> {
    return this.httpClient.delete(this.baseUrl + "InsurancePlan/DeleteInsurancePlan/" + Id);
  }

  GetCompanyUsers(Id: number): Observable<CompanyUsers[]> {
    return this.httpClient.get<CompanyUsers[]>(this.baseUrl + "companies/Users/" + Id);
  }
  GetUserArchive(Id:string)
  {
   
    return this.httpClient.get(this.baseUrl+"companies/UserPdf?id="+Id,{
      responseType: 'blob',
      observe: 'response' 
    });
  }
}
