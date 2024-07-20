import { Component } from '@angular/core';
import { ComparingInsuranceService } from '../../../app/core/services/comparing-insurance.service';
import { InsuranceReview } from '../../../app/core/types/IAllFeedBackes';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { RatingModule } from 'primeng/rating';
import { TagModule } from 'primeng/tag';
import { InputTextModule } from 'primeng/inputtext';
import { PaginatorModule } from 'primeng/paginator';
import { HeaderComponent } from '../../shared/components/header/header.component';

@Component({
  selector: 'app-comparing-insurance',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    TableModule,
    RatingModule,
    TagModule,
    InputTextModule,
    PaginatorModule,
    HeaderComponent,
  ],
  templateUrl: './comparing-insurance.component.html',
  styleUrls: ['./comparing-insurance.component.css'],
})
export class ComparingInsuranceComponent {
  feedbacks: InsuranceReview[] = [];
  processedFeedbacks: any[] = [];
  sortField: string = '';
  sortOrder: number = 1;

  constructor(private comparingInsuranceService: ComparingInsuranceService) {
    this.comparingInsuranceService
      .getAllFeedbacks()
      .subscribe((data: InsuranceReview[]) => {
        this.feedbacks = data;
        this.processFeedbacks();
      });
  }

  processFeedbacks() {
    const feedbackMap: { [key: number]: any } = {};

    this.feedbacks.forEach((feedback) => {
      if (!feedbackMap[feedback.insurancePlanId]) {
        feedbackMap[feedback.insurancePlanId] = {
          insurancePlanId: feedback.insurancePlanId,
          insurancePlanName: feedback.insurancePlanName,
          insurancePlanLevel: feedback.insurancePlanLevel,
          yearlyCoverage: feedback.yearlyCoverage,
          catId: feedback.catId,
          totalRating: 0,
          count: 0,
          avgRating: 0,
        };
      }
      feedbackMap[feedback.insurancePlanId].totalRating += feedback.rating;
      feedbackMap[feedback.insurancePlanId].count += 1;
    });

    this.processedFeedbacks = Object.values(feedbackMap).map((plan) => {
      plan.avgRating = plan.totalRating / plan.count;
      return plan;
    });
  }

  getSeverity(planLevel: string): string {
    switch (planLevel) {
      case 'Premium':
        return 'warning';
      case 'Standard':
        return 'success';
      case 'Basic':
        return 'info';
      default:
        return '';
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
        return 'Unknown';
    }
  }
}
