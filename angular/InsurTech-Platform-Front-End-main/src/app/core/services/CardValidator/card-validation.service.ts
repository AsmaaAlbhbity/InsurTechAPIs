import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BASE_URL } from '../../base-url';

@Injectable({
  providedIn: 'root',
})
export class CardValidationService {
  constructor(private http: HttpClient) {}

  validateCardNumber(cardNumber: string): Observable<boolean> {
    const payload = {
      CardNumber: cardNumber,
      luhnValidateUnionPay: true,
      maxLength: 16,
    };
    console.log('payload from validate card number', payload);

    return this.http.post<boolean>(`${BASE_URL}/CardValidation/validate-card-number`, payload);
    
  }

  validateCardHolderName(cardHolderName: string): Observable<boolean> {
    const payload = { CardHolderName: cardHolderName };
    
    return this.http.post<boolean>(`${BASE_URL}/CardValidation/validate-card-holder-name`, payload);
  }

  validateExpiryDate(expiryDate: string): Observable<boolean> {
    const payload = { ExpiryDate: expiryDate, maxElapsedYear: (new Date().getFullYear() + 10).toString() };
    return this.http.post<boolean>(`${BASE_URL}/CardValidation/validate-expiry-date`, payload);
  }

  validateCvv(cvv: string, maxLength: number = 3): Observable<boolean> {
    const payload = { cvv, maxLength };
    console.log('payload from validate cvv', payload);
    return this.http.post<boolean>(`${BASE_URL}/CardValidation/validate-cvv`, payload);
  }

  validatePostalCode(postalCode: string, minLength: number = 3): Observable<boolean> {
    const payload = { postalCode, minLength };
    return this.http.post<boolean>(`${BASE_URL}/CardValidation/postal-code-check`, payload);
  }
}
