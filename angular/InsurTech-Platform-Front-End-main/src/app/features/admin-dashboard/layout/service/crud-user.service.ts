import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BASE_URL } from '../../../../core/base-url';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private apiBaseUrl = BASE_URL;

  constructor(private http: HttpClient) {}

  getUsers(): Observable<any> {
    return this.http.get<any>(`${this.apiBaseUrl}/Account/GetAllUsers`);
  }

  deleteUser(userId: string, userType: number): Observable<any> {
    const url =
      userType === 1
        ? `${this.apiBaseUrl}/companies/${userId}`
        : `${this.apiBaseUrl}/Customers/DeleteCustomer/${userId}`;
    return this.http.delete<any>(url);
  }

  editUser(user: any, userType: number): Observable<any> {
    const url =
      userType === 1
        ? `${this.apiBaseUrl}/companies/${user.id}`
        : `${this.apiBaseUrl}/Customers/UpdateCustomer/${user.id}`;
    return this.http.put<any>(url, user);
  }
  updateUser(user: any): Observable<any> {
    return this.http.put<any>(`${this.apiBaseUrl}/Customers/UpdateUser`, user);
  }
}
