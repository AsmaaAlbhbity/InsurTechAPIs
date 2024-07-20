import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Forgotpassword } from '../../models/forgotpassword';
import { Resetpassword } from '../../models/resetpassword';
import { BASE_URL } from '../../base-url';

@Injectable({
  providedIn: 'root'
})
export class ForgotpasswordService {

  constructor(private http: HttpClient) {}

  private baseurl: string = `${BASE_URL}/Account/ForgotPasswordAngular`;
  private reseturl:string=`${BASE_URL}/Account/ResetPassword`;
  forgot(email: string): Observable<any> {
    const forgotpass: Forgotpassword = { email };
    return this.http.post<any>(this.baseurl, forgotpass);
  }
  reset(email:string,token:String,password:string): Observable<any>{
    const resetobj:Resetpassword={email,token,password}
    return this.http.post<any>(this.reseturl,resetobj );
  }
}
