import { Component } from '@angular/core';
import { AnimationOptions, LottieComponent } from 'ngx-lottie';

@Component({
  selector: 'app-element-test',
  standalone: true,
  imports: [LottieComponent],
  templateUrl: './element-test.component.html',
  styleUrl: './element-test.component.css'
})
export class ElementTestComponent {
  options: AnimationOptions = {
    path: 'assets/animations/Car Insurance 2.json',
  };
}
