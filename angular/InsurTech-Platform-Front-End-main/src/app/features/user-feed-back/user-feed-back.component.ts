import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ButtonModule } from 'primeng/button';
import { RatingModule } from 'primeng/rating';
import { FloatLabelModule } from 'primeng/floatlabel';
import { InsuranceService } from '../../core/services/GetInsurancePlans.service';
import { AuthService } from '../../core/services/authantication.service';
import { ToastModule } from 'primeng/toast'; // Import ToastModule
import { MessageService } from 'primeng/api'; // Import MessageService

@Component({
  selector: 'app-user-feed-back',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NgbModule,
    InputTextareaModule,
    ButtonModule,
    RatingModule,
    FloatLabelModule,
    ToastModule,
  ],
  providers: [MessageService],
  templateUrl: './user-feed-back.component.html',
  styleUrls: ['./user-feed-back.component.css'],
  encapsulation: ViewEncapsulation.None, // Disable view encapsulation
})
export class UserFeedBackComponent implements OnInit {
  insurancePlans: any[] = [];
  customerId: string | null = null;
  feedbackForms: FormGroup[] = [];
  ratings: number[] = [];

  constructor(
    private insuranceService: InsuranceService,
    private authService: AuthService,
    private fb: FormBuilder,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.customerId = this.authService.getUserId();
    if (this.customerId) {
      this.getUserInsurancePlans();
    } else {
      console.error('Customer ID not found');
    }
  }

  getUserInsurancePlans() {
    if (this.customerId) {
      this.insuranceService.getCustomerRequests(this.customerId).subscribe({
        next: (data: any) => {
          // Filter plans based on status
          this.insurancePlans = data.filter(
            (plan: any) =>
              plan.status === 'Approved' || plan.status === 'Rejected'
          );
          this.insurancePlans.forEach(() => {
            this.feedbackForms.push(
              this.fb.group({
                rating: [0, Validators.required],
                comment: ['', Validators.required],
              })
            );
            this.ratings.push(0); // Initialize ratings array
          });
        },
        error: (error) => {
          console.error('Error fetching insurance plans', error);
        },
      });
    }
  }

  logUserId() {
    const userId = this.authService.getUserId();
    console.log('User ID:', userId);
  }

  submitFeedback(planId: number, index: number) {
    if (this.feedbackForms[index].valid) {
      const feedbackData = {
        insurancePlanId: planId,
        rating: this.ratings[index], // Use individual rating
        comment: this.feedbackForms[index].value.comment,
      };

      this.insuranceService.postFeedback(feedbackData).subscribe({
        next: (response) => {
          console.log('Feedback submitted successfully', response);
          this.messageService.add({
            severity: 'success',
            summary: 'Success',
            detail: 'Your feedback has been sent',
          }); // Display toast message
        },
        error: (error) => {
          console.error('Error submitting feedback', error);
        },
      });
    }
  }

  getCategoryName(catId: number): string {
    switch (catId) {
      case 1:
        return 'Health Insurance';
      case 2:
        return 'Home Insurance';
      case 3:
        return 'Motor Insurance';
      default:
        return 'Unknown Category';
    }
  }
}
