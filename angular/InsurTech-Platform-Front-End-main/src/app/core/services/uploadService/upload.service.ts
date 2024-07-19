import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BASE_URL } from '../../base-url';

@Injectable({
  providedIn: 'root',
})
export class UploadService {
  // private apiUrl = `${BASE_URL}/upload/image`;
  private apiUrl = `${BASE_URL}`;

  constructor(private http: HttpClient) {}

  uploadFile(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file, file.name);

    return this.http.post(`${this.apiUrl}/profile/image`, formData);
  }

  getCustomerInfo() {
    return this.http.get(`${this.apiUrl}/customers/profile`);
  }
}
