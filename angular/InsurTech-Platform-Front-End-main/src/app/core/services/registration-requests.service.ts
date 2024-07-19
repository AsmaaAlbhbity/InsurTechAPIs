import { Inject, Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { BASE_URL } from '../base-url';


@Injectable({
  providedIn: 'root'
})
export class RegistrationRequestsServiceService {
  
  constructor( private http:HttpClient) { }

  GetAll(){
    return this.http.get(BASE_URL + '/companies');
  }

  GetById(id: number){
    return this.http.get(BASE_URL + '/companies/' + id);
  }

  ApproveRequest(id: number){
    return this.http.post(BASE_URL + '/companies/ApproveCompany/' + id, {});
  }

  RejectRequest(id: number){
    return this.http.post(BASE_URL + '/companies/RejectCompany/' + id, {});
  }
}
