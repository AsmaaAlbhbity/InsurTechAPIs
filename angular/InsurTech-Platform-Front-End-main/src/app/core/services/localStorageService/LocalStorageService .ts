import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {
  saveCardInfo(cardInfo: any) {
    localStorage.setItem('cardInfo', JSON.stringify(cardInfo));
  }

  getCardInfo() {
    return JSON.parse(localStorage.getItem('cardInfo') || '{}');
  }

  saveCardLastFour(lastFour: string) {
    localStorage.setItem('cardLastFour', lastFour);
  }

  getCardLastFour() {
    return localStorage.getItem('cardLastFour');
  }
}
