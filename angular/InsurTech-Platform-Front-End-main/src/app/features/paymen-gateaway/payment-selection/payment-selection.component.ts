import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-payment-selection',
  templateUrl: './payment-selection.component.html',
  styleUrls: ['./payment-selection.component.css']
})
export class PaymentSelectionComponent {
  @Input() plan: any;
  @Input() catId: number = 0;

  constructor(private router: Router) {}

  proceedToPayment() {
    this.router.navigate(['payment'], { state: { plan: this.plan, catId: this.catId } });
  }
}
