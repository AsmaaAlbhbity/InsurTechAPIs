import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { StepperModule } from 'primeng/stepper';
import { InputTextModule } from 'primeng/inputtext';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-steps',
  standalone: true,
  imports: [
    StepperModule,
    ButtonModule,
    InputTextModule,
    ToggleButtonModule,
    IconFieldModule,
    InputIconModule,
    CommonModule,
    FormsModule
  ],
  templateUrl: './steps.component.html',
  styleUrl: './steps.component.css'
})
export class StepsComponent {
  active: number = 0;
  themeColor: string = '#45644c'; // Green color

  option1: boolean | undefined = false;
  option2: boolean | undefined = false;
  option3: boolean | undefined = false;
  option4: boolean | undefined = false;
  option5: boolean | undefined = false;
  option6: boolean | undefined = false;
  option7: boolean | undefined = false;
  option8: boolean | undefined = false;
  option9: boolean | undefined = false;
  option10: boolean | undefined = false;

  setActiveStep(step: number) {
    this.active = step;
  }

  isOptionSelected(option: boolean | undefined): boolean {
    return !!option;
  }
  isFirstStep(): boolean {
    return this.active === 0;
  }

  isLastStep(): boolean {
    // Adjust the number of steps based on your implementation
    return this.active === 2; // Assuming you have 3 steps (0-based index)
  }
}
