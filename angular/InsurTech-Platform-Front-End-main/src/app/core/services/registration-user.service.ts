import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { registerUser } from '../services/iregistration-user.service';
import { BASE_URL } from '../base-url';

@Injectable({
  providedIn: 'root',
})
export class RegistrationUserService {
  private endpoints = {
    getByEmail: `${BASE_URL}/Customers/GetCustomerByEmail`,
    getByUsername: `${BASE_URL}/Customers/GetCustomerByUsername`,
    getByNationalId: `${BASE_URL}/Customers/GetCustomerByNationalId`,
    registerCustomer: `${BASE_URL}/Customers/RegisterCustomer`,
  };

  constructor(private http: HttpClient) {}

  registerCustomer(user: registerUser): Observable<any> {
    return this.http
      .post<any>(this.endpoints.registerCustomer, user)
      .pipe(catchError(this.handleError));
  }

  checkEmailAvailability(email: string): Observable<any> {
    return this.checkAvailability(this.endpoints.getByEmail, email, 'email');
  }

  checkUsernameAvailability(username: string): Observable<any> {
    return this.checkAvailability(
      this.endpoints.getByUsername,
      username,
      'username'
    );
  }

  checkNationalIdAvailability(nationalId: string): Observable<any> {
    return this.checkAvailability(
      this.endpoints.getByNationalId,
      nationalId,
      'national ID'
    );
  }

  private checkAvailability(
    endpoint: string,
    value: string,
    type: string
  ): Observable<any> {
    console.log(`Checking ${type} availability for: ${value}`);
    return this.http.get<any>(`${endpoint}/${encodeURIComponent(value)}`).pipe(
      map((response) => {
        console.log(`${type} availability response: ${response}`);
        return response;
      }),
      catchError((error: HttpErrorResponse) => {
        console.error(
          `${type} validation error: ${this.getErrorMessage(error)}`
        );
        if (error.status === 404) {
          return of(null); // Value not found, hence available
        }
        return throwError(
          () => new Error(`Error checking ${type} availability`)
        );
      })
    );
  }

  handleError(error: HttpErrorResponse) {
    let errorMessage = 'An unknown error occurred!';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `A client-side error occurred: ${error.error.message}`;
    } else {
      errorMessage = `Server returned code: ${error.status}, error message is: ${error.message}`;
    }
    console.error(errorMessage);
    return throwError(() => new Error(errorMessage));
  }

  getErrorMessage(error: HttpErrorResponse): string {
    return error.error ? JSON.stringify(error.error) : error.message;
  }
}
