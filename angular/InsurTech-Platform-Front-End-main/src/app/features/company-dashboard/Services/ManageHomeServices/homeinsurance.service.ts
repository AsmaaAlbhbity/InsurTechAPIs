import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { AddHomeInsurance } from '../../Model/Homeinsurance/add-home-insurance';
import { EditHomeInsurance } from '../../Model/Homeinsurance/edit-home-insurance';
import { Observable, Subject, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { BASE_URL } from '../../../../core/base-url';

@Injectable({
  providedIn: 'root'
})
export class HomeinsuranceService {
  private baseurl: string = `${BASE_URL}/HomeInsurance/`;

  constructor(private httpClient: HttpClient) { }

  private homeinsurancechanges=new Subject<void>();
  homeinsurancechanges$=this.homeinsurancechanges.asObservable();

  add(homeinsurance: AddHomeInsurance): Observable<any> {
    return this.httpClient.post<any>(`${this.baseurl}AddHomePlan`, homeinsurance)
      .pipe(
        catchError(this.handleError),
        tap(()=>this.homeinsurancechanges.next())
      )
  }

  // Edit
  edit(homeinsurance: EditHomeInsurance): Observable<any> {
    return this.httpClient.put<any>(`${this.baseurl}${homeinsurance.id}`, homeinsurance)
      .pipe(
        catchError(this.handleError),
        tap(()=>this.homeinsurancechanges.next())
      );
  }

  // Get by ID
  getById(id: number): Observable<any> {
    return this.httpClient.get<any>(`${this.baseurl}${id}`);
  }
    // Get by ID
    getAll(): Observable<any> {
      return this.httpClient.get<any>(`${BASE_URL}/HomeInsurance/GetHomeInsurance`)
        .pipe(
          map(response => response.result),
          catchError(this.handleError)
        );
    }

  // Error handling
  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'An unknown error occurred!';
    if (error.error instanceof ErrorEvent) {
      // Client-side errors
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side errors
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.error(errorMessage);
    return throwError(errorMessage);
  }
}
