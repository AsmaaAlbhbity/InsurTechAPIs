import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { InsurancePlanService } from '../../../core/services/insurancePlan.service';
import { QuestionsFormService } from '../../../core/services/questions-form.service';

@Component({
  selector: 'app-insurance-plan-card',
  standalone: true,
  templateUrl: './insurance-plan-card.component.html',
  styleUrls: ['./insurance-plan-card.component.css']
})
export class InsurancePlanCardComponent {
  @Input() plan: any;
  @Input() catId: number = 0;
  @Input() badgeColor: string= '';
  showMore = false;

  constructor(
    private questionService: QuestionsFormService,
    private router: Router,



    private insurancePlanService:InsurancePlanService
  ) {
  }
 ngOnChanges(): void {
  console.log("plan", this.plan);

  
 }
  toggleMore() {
    this.showMore = !this.showMore;
  }


  createRequest() {

    this.insurancePlanService.SendRequestInsurancePlan(this.plan.id, this.questionService.GetAnswers()).subscribe({
      next: data => {
        this.router.navigate([
          'successpurchasing',
          this.plan.id,
          this.catId,
        ]);
        console.log(this.plan.id, this.catId);
      },
      error: error => {
        console.error('There was an error!', error);
      }
    });
    //  this.router.navigate(['payment'], { state: { plan: this.plan, answers: this.questionService.GetAnswers() } });
    // this.createRequest();
  }}

 
