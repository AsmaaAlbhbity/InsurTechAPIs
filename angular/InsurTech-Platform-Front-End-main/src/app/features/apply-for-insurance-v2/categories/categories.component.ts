import { Component } from '@angular/core';
import { RouterLink, RouterModule } from '@angular/router';
import { HeaderComponent } from '../../../shared/components/header/header.component';
import { AnimationOptions, LottieComponent } from 'ngx-lottie';

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [RouterModule, RouterLink, HeaderComponent, LottieComponent],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.css'
})
export class CategoriesComponent {

  HomeOptions: AnimationOptions = {
    path: '/assets/animations/Home Insurance.json',
  };

  MotorOptions: AnimationOptions = {
    path: '/assets/animations/Car Insurance.json',
  };

  HealthOptions: AnimationOptions = {
    path: '/assets/animations/Health Insurance.json',
  };
}
