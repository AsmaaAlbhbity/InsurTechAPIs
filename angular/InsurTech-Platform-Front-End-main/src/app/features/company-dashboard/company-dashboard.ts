import { Component} from '@angular/core';
import { ListInsurancePlanComponent } from './Componants/list-insurance-plan/list-insurance-plan.component';
import { CardsComponent } from './Componants/cards/cards.component';
import { SidenavComponent } from './Componants/sidenav/sidenav.component';
import { ChartCompanyComponent } from './Componants/chart-company/chart-company.component';
import { UsersCompanyComponent } from './Componants/users-company/users-company.component';
import { RouterModule } from '@angular/router';



@Component({
  selector: 'app-companydasboard',
  standalone: true,
  imports: [ListInsurancePlanComponent,CardsComponent,SidenavComponent,ChartCompanyComponent,UsersCompanyComponent, RouterModule],
  templateUrl: './company-dashboard.component.html',
  styleUrl: './company-dashboard.component.css'
})
export class companydashbordcomponant {
 

}
