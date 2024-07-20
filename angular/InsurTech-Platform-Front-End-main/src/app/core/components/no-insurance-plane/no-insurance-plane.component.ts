import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { HeaderComponent } from '../../../shared/components/header/header.component';

@Component({
  selector: 'app-no-insurance-plane',
  standalone: true,
  imports: [RouterLink, HeaderComponent],
  templateUrl: './no-insurance-plane.component.html',
  styleUrl: './no-insurance-plane.component.css'
})
export class NoInsurancePlaneComponent {

}
