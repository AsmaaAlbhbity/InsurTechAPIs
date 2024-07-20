import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
// import { text } from '@fortawesome/free-solid-svg-icons';
import { BASE_URL } from '../../base-url';
import { Userlogin } from '../../models/userlogin';
import { Googlelogin } from '../../models/googlelogin';



@Injectable({
  providedIn: 'root'
})
export class LoginService {
    private baseurl:string=`${BASE_URL}/Account/Login`
    constructor(public http: HttpClient) {}
    user:Userlogin=new Userlogin("","")
    login(email: string, password: string): Observable<any> {
      // debugger
        this.user.email=email;
        this.user.password=password;
      return this.http.post<any>(this.baseurl,this.user );
    }
    private googleurl:string=`${BASE_URL}/Account/GooglleLogin`
    googlelog(Text:Googlelogin): Observable<any>{
      return this.http.post<any>(this.googleurl,Text );
    }

}
