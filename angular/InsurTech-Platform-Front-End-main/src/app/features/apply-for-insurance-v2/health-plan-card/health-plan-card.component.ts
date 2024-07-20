import { Component, Input } from '@angular/core';
import { QuestionsFormService } from '../../../core/services/questions-form.service';
import { InsurancePlanService } from '../../../core/services/insurancePlan.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-health-plan-card',
  standalone: true,
  imports: [],
  templateUrl: './health-plan-card.component.html',
  styleUrl: './health-plan-card.component.css'
})
export class HealthPlanCardComponent {
 @Input() data:any
  showMore = false;
  toggleMore() {
    this.showMore = !this.showMore;
  }
  constructor(private insurancePlanService: InsurancePlanService, private questionService: QuestionsFormService, private router: Router) 
  {}
  ngOnChanges(): void {
    console.log('data from health Plan', this.data);
  }
  createRequest() {
    this.insurancePlanService.SendRequestInsurancePlan(this.data.id, this.questionService.GetAnswers()).subscribe({
      next: data => {
        this.router.navigate(['success'])  },
      error: error => {
        console.error('There was an error!', error);
      }
    });
  }
}
