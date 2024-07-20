import { Component } from '@angular/core';
import { HomeinsuranceComponent } from '../Homeinsurance/homeinsurance/homeinsurance.component';
import { HealthinsuranceComponent } from '../Helathinsurance/healthinsurance/healthinsurance.component';
import { MotorinsuranceComponent } from '../Motorinsurance/motorinsurance/motorinsurance.component';
// import { LazyLoadImageDirective, LazyLoadImageModule } from 'ng-lazyload-image';
import { RouterModule, RouterOutlet } from '@angular/router';
import { SidenavComponent } from '../sidenav/sidenav.component';

@Component({
  selector: 'app-insurancecollection',
  standalone: true,
  imports: [
    HomeinsuranceComponent,
    HealthinsuranceComponent,
    MotorinsuranceComponent,
    RouterOutlet,
    SidenavComponent,
    RouterModule,
  ],
  templateUrl: './insurancecollection.component.html',
  styleUrl: './insurancecollection.component.css',
})
export class InsurancecollectionComponent {}
