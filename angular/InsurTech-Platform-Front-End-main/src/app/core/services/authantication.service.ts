import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  private userDataSubject = new BehaviorSubject<any>(null);

  constructor() {
    this.checkLoginStatus();
  }

  login(userData: any): void {
    this.isLoggedInSubject.next(true);
    this.userDataSubject.next(userData);
    localStorage.setItem('userData', JSON.stringify(userData));
  }

  logout(): void {
    this.isLoggedInSubject.next(false);
    this.userDataSubject.next(null);
    localStorage.removeItem('userData');
  }

  isLoggedIn(): Observable<boolean> {
    return this.isLoggedInSubject.asObservable();
  }

  getUserData(): Observable<any> {
    return this.userDataSubject.asObservable();
  }

  checkLoginStatus(): void {
    const userData = localStorage.getItem('userData');
    if (userData) {
      this.isLoggedInSubject.next(true);
      this.userDataSubject.next(JSON.parse(userData));
    } else {
      this.isLoggedInSubject.next(false);
      this.userDataSubject.next(null);
    }
  }
  getUserId(): string | null {
    const userData = localStorage.getItem('userData');
    if (userData) {
      const parsedData = JSON.parse(userData);
      return parsedData.id; // Ensure this matches your actual data structure
    }
    return null;
  }
}
