import { Component } from '@angular/core';
import { InsurancePlanCardComponent } from '../insurance-plan-card/insurance-plan-card.component';
import { QuestionsFormService } from '../../../core/services/questions-form.service';
import { Router } from '@angular/router';
import { InsurancePlanService } from '../../../core/services/insurancePlan.service';
import { HomePlanCardComponent } from '../home-plan-card/home-plan-card.component';
import { HealthPlanCardComponent } from '../health-plan-card/health-plan-card.component';
import { MotorPlanCardComponent } from '../motor-plan-card/motor-plan-card.component';
import { NoInsurancePlaneComponent } from '../../../core/components/no-insurance-plane/no-insurance-plane.component';
import { HeaderComponent } from '../../../shared/components/header/header.component';
import { FooterComponent } from '../../../shared/components/footer/footer.component';

@Component({
  selector: 'app-insurance-plans',
  standalone: true,
  imports: [
    InsurancePlanCardComponent,
    HomePlanCardComponent,
    HealthPlanCardComponent,
    MotorPlanCardComponent,
    NoInsurancePlaneComponent,
    HeaderComponent,
    FooterComponent,
  ],
  templateUrl: './insurance-plans.component.html',
  styleUrl: './insurance-plans.component.css',
})
export class InsurancePlansComponent {
  catId: number = 0;
  data: any = null;
  colorMap: { [key: string]: string } = {};
  colorIndex: number = 0;
  colors: string[] = [
    'badge-primary',
    'badge-secondary',
    'badge-dark',
    'badge-danger',
    'badge-warning',
  ];
  constructor(
    private questionService: QuestionsFormService,
    private router: Router,
    private insurancePlanService: InsurancePlanService
  ) {
    this.catId = parseInt(sessionStorage.getItem('catId') || '0');
    console.log('catId', this.catId);
  }
  ngOnInit(): void {
    if (this.catId == 0) {
      this.router.navigate(['apply-for-insuranceV2']);
      console.log('catId', this.catId);
    }
    this.insurancePlanService
      .GetInsurancePlanByCategory(this.catId)
      .subscribe((data) => {
       // console.log('data FOR retrieving insuarnce plans', data);
        this.data=this.rearrangeInsurancePlans(data);
        //this.data = data;
      });
  }
  getBadgeColor(company: string): string {
    if (!this.colorMap[company]) {
      this.colorMap[company] = this.colors[this.colorIndex % this.colors.length];
      this.colorIndex++;
    }
    return this.colorMap[company];
  }
  //function to rearrange the insurance plans based on the level of coverage
  rearrangeInsurancePlans(data: any): [] {
    let plans: any = [];
    let level1: any = [];
    let level2: any = [];
    let level3: any = [];
    console.log('data from rearrange', data);
    data.forEach((plan: any) => {
      if (plan.level == 0) {
        level1.push(plan);
      } else if (plan.level == 1) {
        level2.push(plan);
      } else if (plan.level == 2) {
        level3.push(plan);
      }
    });

    plans = level1.concat(level2).concat(level3);
    console.log('plans from rearrange', plans);
    return plans;
   
  }
}
