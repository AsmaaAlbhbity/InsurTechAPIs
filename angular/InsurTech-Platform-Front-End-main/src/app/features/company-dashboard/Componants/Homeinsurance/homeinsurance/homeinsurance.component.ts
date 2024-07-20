import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeinsuranceService } from '../../../Services/ManageHomeServices/homeinsurance.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AddHomeInsurance ,InsurancePlanLevel} from '../../../Model/Homeinsurance/add-home-insurance'; // Corrected import path
import { ActivatedRoute } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api'
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';

@Component({
  selector: 'app-homeinsurance',
  standalone: true,
  providers: [MessageService, ConfirmationService],
  imports: [ReactiveFormsModule, CommonModule,ToastModule,ConfirmDialogModule],
  templateUrl: './homeinsurance.component.html',
  styleUrl: './homeinsurance.component.css'


})

export class HomeinsuranceComponent {
  InsurancePlanLevel = InsurancePlanLevel; // Expose enum to the template

  addhomeinsurance: FormGroup = new FormGroup({
    yearlyCoverage: new FormControl(null, [Validators.required, Validators.min(0), Validators.max(1.7976931348623157e+308)]),
    level: new FormControl(this.InsurancePlanLevel.Basic, [Validators.required]),
    quotation: new FormControl(null, [Validators.required, Validators.min(0), Validators.max(1.7976931348623157e+308)]),
    waterDamage: new FormControl(null, [Validators.required, Validators.min(0), Validators.max(1.7976931348623157e+308)]),
    glassBreakage: new FormControl(null, [Validators.required, Validators.min(0), Validators.max(1.7976931348623157e+308)]),
    naturalHazard: new FormControl(null, [Validators.required, Validators.min(0), Validators.max(1.7976931348623157e+308)]),
    attemptedTheft: new FormControl(null, [Validators.required, Validators.min(0), Validators.max(1.7976931348623157e+308)]),
    firesAndExplosion: new FormControl(null, [Validators.required, Validators.min(0), Validators.max(1.7976931348623157e+308)])
  });
  userId: string = JSON.parse(localStorage.getItem('userData') || "{}").id;

  constructor(public homeinsuranceService: HomeinsuranceService,public route : ActivatedRoute,private messageService: MessageService,
  ) {}
  ngOnInit(): void {
    

    console.log('User ID:', this.userId); // This will log the user ID to the console
  }
  addhome() {
    if (this.addhomeinsurance.valid) {
      const formValue = this.addhomeinsurance.value;
      const newAdd: AddHomeInsurance = new AddHomeInsurance(
        formValue.yearlyCoverage,
        formValue.level,
        formValue.quotation,
        this.userId,
        formValue.waterDamage,
        formValue.glassBreakage,
        formValue.naturalHazard,
        formValue.attemptedTheft,
        formValue.firesAndExplosion
      );
  
      console.log('Payload:', newAdd); // Log the payload
  
      this.homeinsuranceService.add(newAdd).subscribe(
        response => {
          console.log('Home insurance added successfully', response);
          this.addhomeinsurance.reset();
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Insurance plan Added' });
        },
        error => {
          this.messageService.add({ severity: 'error', summary: 'Rejected', detail: 'Error Occured Try again later' });
        }
      );
    } else {
      console.error('Form is invalid');
    }
  }
  

}



