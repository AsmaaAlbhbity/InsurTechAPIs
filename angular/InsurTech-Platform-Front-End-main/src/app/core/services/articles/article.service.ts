import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BASE_URL } from '../../../core/base-url';


@Injectable({
  providedIn: 'root'
})
export class ArticaldataService {

  constructor(public http: HttpClient) { }
  private baseurl:string=`${BASE_URL}/articles`



  getArticleData(): Observable<any> {
   
    return this.http.get<any>(this.baseurl);
  }
}
