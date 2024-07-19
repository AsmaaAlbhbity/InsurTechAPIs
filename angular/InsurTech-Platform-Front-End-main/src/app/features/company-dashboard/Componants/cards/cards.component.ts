import { Component, OnDestroy, OnInit } from '@angular/core';
import { CompanyService } from '../../Services/CompanyServices/company.service';
import { Subscription } from 'rxjs';
import { ListInsurancePlan } from '../../Model/company/ListInsurancePlan'; 

@Component({
  selector: 'app-cards',
  standalone: true,
  imports: [],
  templateUrl: './cards.component.html',
  styleUrls: ['./cards.component.css'] 
})
export class CardsComponent implements OnInit, OnDestroy {
  public HealthNumber: number = 0;
  public HomeNumber: number = 0; 
  public MotorNumber: number = 0;
  public RequestNumber: number = 0;
  private Id =JSON.parse(localStorage.getItem('userData') || '{id: 1}').id;  
  subscriptions: Subscription[] = []; 

  constructor(public CompanyServices: CompanyService) { }

  ngOnInit(): void {
    this.CompanyServices.GetAll(this.Id);
    this.subscriptions.push(
      this.CompanyServices.insurancePlans$.subscribe({
        next: (data: ListInsurancePlan | null) => {
          if (data) {
            this.RequestNumber = 0;
            this.HealthNumber = data.healthInsurancePlans?.length ?? 0;
            this.HomeNumber = data.homeInsurancePlans?.length ?? 0;
            this.MotorNumber = data.motorInsurancePlans?.length ?? 0;

            this.RequestNumber += data.healthInsurancePlans?.reduce((sum, plan) => sum + (plan.numberOfUsers || 0), 0) ?? 0;
            this.RequestNumber += data.homeInsurancePlans?.reduce((sum, plan) => sum + (plan.numberOfUsers || 0), 0) ?? 0;
            this.RequestNumber += data.motorInsurancePlans?.reduce((sum, plan) => sum + (plan.numberOfUsers || 0), 0) ?? 0;
          }
        },
        error: (error: any) => { console.log(error); } 
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(sub => sub.unsubscribe()); 
  }
}
