import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { Router } from '@angular/router';
import { HeaderComponent } from '../../../shared/components/header/header.component';

import { FooterComponent } from '../../../shared/components/footer/footer.component';
import { AboutComponent } from '../../HomePage/about/about.component';


@Component({
  selector: 'app-display',
  standalone: true,

  imports: [DialogModule, ButtonModule, HeaderComponent, AboutComponent],

  templateUrl: './display.component.html',
  styleUrls: ['./display.component.css'],
})
export class DisplayComponent {
  visible: boolean = false; 
  hoverIndex: number | null = null;

  constructor(private router: Router) {}

  showHealthInsurance() {
    this.router.navigate(['/insurance/health']);
  }

  showHomeInsurance() {
    this.router.navigate(['/insurance/home']);
  }

  showMotorInsurance() {
    this.router.navigate(['/insurance/motor']);
  }

  
}
