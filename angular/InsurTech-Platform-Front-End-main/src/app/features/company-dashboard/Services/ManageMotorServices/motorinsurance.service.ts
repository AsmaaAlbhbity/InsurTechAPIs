import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import{ AddMotorInsurance } from '../../Model/Motorinsurance/add-motor-insurance'
import { FormGroup } from '@angular/forms';
import { Observable, Subject, tap } from 'rxjs';
import { EditMotorInsurance } from '../../Model/Motorinsurance/edit-motor-insurance';
import { BASE_URL } from '../../../../core/base-url';
@Injectable({
  providedIn: 'root'
})

export class MotorinsuranceService {
 
  private baseurl: string = `${BASE_URL}/MotorInsurance/`;
  private motorinsurancechanges=new Subject<void>();
  motorinsurancechanges$=this.motorinsurancechanges.asObservable();

  constructor(public httpClient :HttpClient ) { }
  addmotor(motorinsurance: AddMotorInsurance): Observable<any> {
    return this.httpClient.post<any>(`${this.baseurl}AddMotorPlan`, motorinsurance).pipe(
      tap(()=>{this.motorinsurancechanges.next()})
    )

   }
   
   edit(motorinsurance: EditMotorInsurance): Observable<any> {
    return this.httpClient.put<any>(`${this.baseurl}EditMotorPlan/${motorinsurance.id}`, motorinsurance).pipe(
      tap(()=>{this.motorinsurancechanges.next()}));
  }
  

  getById(id: string): Observable<any> {
    return this.httpClient.get<any>(`${this.baseurl}${id}`);
  }
}
