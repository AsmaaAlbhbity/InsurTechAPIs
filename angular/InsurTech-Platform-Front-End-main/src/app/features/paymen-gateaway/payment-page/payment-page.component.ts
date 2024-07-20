import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { ExpirationDateMaskDirective } from '../../../shared/directives/expiration-date-mask.directive';
import { CreditCardMaskDirective } from '../../../shared/directives/credit-card-mask.directive';
import { CvvMaskDirective } from '../../../shared/directives/cvv-mask.directive';
import { LocalStorageService } from '../../../core/services/localStorageService/LocalStorageService ';
import { InsurancePlanService } from '../../../core/services/insurancePlan.service';
import { QuestionsFormService } from '../../../core/services/questions-form.service';
import { CardValidationService } from '../../../core/services/CardValidator/card-validation.service';
import { forkJoin } from 'rxjs';
import { HeaderComponent } from '../../../shared/components/header/header.component';

@Component({
  selector: 'app-payment-page',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CreditCardMaskDirective,
    ExpirationDateMaskDirective,
    HeaderComponent
  ],
  templateUrl: './payment-page.component.html',
  styleUrls: ['./payment-page.component.css'],
})
export class PaymentPageComponent implements OnInit {
  paymentForm: FormGroup = new FormGroup({});
  savedCardLastFour: string = '';
  plan: any;
  answers: any;

  constructor(
    private fb: FormBuilder,
    private localStorageService: LocalStorageService,
    private router: Router,
    private insurancePlanService: InsurancePlanService,
    private cardValidationService: CardValidationService
  ) {
    const navigation = this.router.getCurrentNavigation();
    console.log('navigation', navigation);
    if (navigation && navigation.extras.state) {
      const state = navigation.extras.state as { plan: any; answers: any };
      this.plan = state.plan;
      this.answers = state.answers;
      console.log('plan', this.plan);
    }
  }

  ngOnInit(): void {
    this.initForm();
    this.loadSavedCard();
  }

  initForm() {
    this.paymentForm = this.fb.group({
      cardNumber: [
        '',
        [Validators.required, Validators.pattern(/^\d{4} \d{4} \d{4} \d{4}$/)],
      ],
      expirationDate: [
        '',
        [Validators.required, Validators.pattern(/^(0[1-9]|1[0-2])\/\d{2}$/)],
      ],
      cvv: ['', [Validators.required, Validators.pattern(/^\d{3}$/)]],
      name: ['', Validators.required],
    });
  }

  loadSavedCard() {
    this.savedCardLastFour =
      this.localStorageService.getCardLastFour() || '5949';
    const savedCard = this.localStorageService.getCardInfo();
    if (savedCard) {
      this.paymentForm.patchValue(savedCard);
    }
  }

  saveCard() {
    const cardNumber = this.paymentForm
      .get('cardNumber')
      ?.value.replace(/\s+/g, ''); // Remove spaces
    const lastFour = cardNumber.slice(-4);
    this.localStorageService.saveCardInfo({
      ...this.paymentForm.value,
      cardNumber,
    }); // Save without spaces
    this.localStorageService.saveCardLastFour(lastFour);
  }

  validateCardDetails() {
    const cardNumber = this.paymentForm
      .get('cardNumber')
      ?.value.replace(/\s+/g, ''); // Remove spaces
    const cardHolderName = this.paymentForm.get('name')?.value;
    const expiryDate = this.paymentForm.get('expirationDate')?.value;
    const cvv = this.paymentForm.get('cvv')?.value;

    forkJoin({
      isCardNumberValid:
        this.cardValidationService.validateCardNumber(cardNumber),
      isCardHolderNameValid:
        this.cardValidationService.validateCardHolderName(cardHolderName),
      isExpiryDateValid:
        this.cardValidationService.validateExpiryDate(expiryDate),
      isCvvValid: this.cardValidationService.validateCvv(cvv),
    }).subscribe(
      (results) => {
        if (
          results.isCardNumberValid &&
          results.isCardHolderNameValid &&
          results.isExpiryDateValid &&
          results.isCvvValid
        ) {
          Swal.fire({
            icon: 'success',
            title: 'Payment Successful',
            text: 'Your payment has been processed successfully!',
          }).then(() => {
            this.createRequest();
          });
        } else {
          Swal.fire({
            icon: 'error',
            title: 'Validation Error',
            text: 'Invalid card details. Please check your information and try again.',
          });
        }
      },
      (error) => {
        console.error('Validation error:', error);
        Swal.fire({
          icon: 'error',
          title: 'Validation Error',
          text: 'An error occurred during validation. Please try again.',
        });
      }
    );
  }

  onSubmit() {
    if (this.paymentForm.valid) {
      this.validateCardDetails();
    } else {
      this.paymentForm.markAllAsTouched();
      Swal.fire({
        icon: 'error',
        title: 'Form Error',
        text: 'Please fill out the form correctly.',
      });
    }
  }
  createRequest() {
    if (!this.plan ) {
      console.error('Plan  are required');
      //this.router.navigate(['successpurchasing']);
      return;
    }

    this.insurancePlanService
      .changePaidToTrue(this.plan.planId)
      .subscribe({
        next: (data) => {
          this.router.navigate(['successpurchasing/'+this.plan.planId+'/'+this.plan.catId]);
        },
        error: (error) => {

          console.error('There was an error!', error);
          console.log(this.plan)
          Swal.fire({
            icon: 'error',
            title: 'Request Failed',
            text: 'There was an error processing your request. Please try again.',
          }).then(() => {
            // this.router.navigate(['successpurchasing']);
          });
        },
      });
  }
}
