import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { CategoriesService } from '../../../core/services/categories.service';


export interface InsurancePlanAndUserNameDTO {
  insurancePlan: InsurancePlanForCompanyDTO;
  usernames: string[];
}

export interface InsurancePlanForCompanyDTO {
  categoryName: string;
  level: string;
  quotation: number;
}


@Component({
  selector: 'app-company-insurance-plan',
  standalone: true,
  imports: [DialogModule, ButtonModule, CommonModule],
  templateUrl: './company-insurance-plan.component.html',
  styleUrls: ['./company-insurance-plan.component.css']
})
export class CompanyInsurancePlanComponent implements OnInit {
  visible: boolean = false;
  hoverIndex: number | null = null;
  companies: any[] = []; 
  insurancePlans: any[] = []; 
  users: string[] = [];
  totalUsers: number = 0;
  plan:any;

  constructor(private categoriesService: CategoriesService) {}

  ngOnInit() : void{
    const userData = JSON.parse(sessionStorage.getItem('userData') || '{}');
    const companyId = userData.id ; 

    this.getCompanyPlans(companyId);
  }

  getCompanyPlans(companyId: string) {
    this.categoriesService.getInsurancePlans(companyId).subscribe(
      (data) => {
        this.insurancePlans = data;
        console.log('Insurance plans for company ID:', companyId, data);
      },
      (error) => {
        console.error('Error fetching insurance plans:', error);
      }
    );
  }

 

 showUserInfo(insuranceId: number) {
   
         console.log('User info received:',  this.insurancePlans[insuranceId].usernames);

         this.users =  this.insurancePlans[insuranceId].usernames;

         this.totalUsers = this.users.length;

         this.visible = true;
        
   
  }



}


