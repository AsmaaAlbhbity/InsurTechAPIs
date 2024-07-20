import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ProfileComponent } from '../profile/profile.component';
import { HeaderComponent } from '../../../shared/components/header/header.component';

@Component({
  selector: 'app-user-dashboard',
  standalone: true,
  imports: [RouterModule, ProfileComponent,HeaderComponent],
  templateUrl: './user-dashboard.component.html',
  styleUrl: './user-dashboard.component.css'
})
export class UserDashboardComponent {

}
