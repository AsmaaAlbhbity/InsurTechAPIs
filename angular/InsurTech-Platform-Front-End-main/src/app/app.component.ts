import { Component } from '@angular/core';
import { RouterLink, RouterOutlet , RouterModule } from '@angular/router';
import { AppLayoutModule } from './features/admin-dashboard/layout/app.layout.module';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    AppLayoutModule,
    RouterLink,
    RouterModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'InsurTech';
}