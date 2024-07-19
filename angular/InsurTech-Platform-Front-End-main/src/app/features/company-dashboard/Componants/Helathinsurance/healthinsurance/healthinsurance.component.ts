
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { HealthinsuranceService } from '../../../Services/ManageHealthServices/healthinsurance.service';
import { AddHealthInsurance, InsurancePlanLevel } from '../../../Model/Helathinsurance/add-health-insurance';
import { ConfirmationService, MessageService } from 'primeng/api'
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';

@Component({
  selector: 'app-healthinsurance',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule,ToastModule,ConfirmDialogModule],
  providers: [MessageService, ConfirmationService],
  templateUrl: './healthinsurance.component.html',
  styleUrls: ['./healthinsurance.component.css']
})
export class HealthinsuranceComponent implements OnInit {
  addhealthform!: FormGroup;
  InsurancePlanLevel = InsurancePlanLevel;
  userId: string = JSON.parse(localStorage.getItem('userData') || "{}").id ;

  constructor(public healthService: HealthinsuranceService, private messageService: MessageService,
  ) { }

  ngOnInit(): void {
    this.addhealthform = new FormGroup({
      yearlyCoverage: new FormControl(null, [Validators.required, Validators.min(0), Validators.max(1.7976931348623157e+308)]),
      level: new FormControl(this.InsurancePlanLevel.Basic, [Validators.required]),
      quotation: new FormControl(null, [Validators.required, Validators.min(0), Validators.max(1.7976931348623157e+308)]),
      medicalNetwork: new FormControl(null, [Validators.required]),
      clinicsCoverage: new FormControl(null, [Validators.required, Validators.min(0), Validators.max(1.7976931348623157e+308)]),
      hospitalizationAndSurgery: new FormControl(null, [Validators.required, Validators.min(0), Validators.max(1.7976931348623157e+308)]),
      opticalCoverage: new FormControl(null, [Validators.required, Validators.min(0), Validators.max(1.7976931348623157e+308)]),
      dentalCoverage: new FormControl(null, [Validators.required, Validators.min(0), Validators.max(1.7976931348623157e+308)])
    });
  }

  addhealth() {
    if (this.addhealthform.valid) {
      const formValue = this.addhealthform.value;
      const healthObj: AddHealthInsurance = new AddHealthInsurance(
        formValue.yearlyCoverage,
        formValue.level,
        formValue.quotation,
        this.userId, // Use the userId value here
        formValue.medicalNetwork,
        formValue.clinicsCoverage,
        formValue.hospitalizationAndSurgery,
        formValue.opticalCoverage,
        formValue.dentalCoverage
      );

      console.log("Health level: ", healthObj.level);
      console.log('Payload:', healthObj);

      this.healthService.add(healthObj).subscribe(
        response => {
          console.log('Health insurance added successfully', response);
          this.addhealthform.reset();
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Insurance plan Added' });
        },
        error => {
          this.messageService.add({ severity: 'error', summary: 'Rejected', detail: 'Error Occured Try again later' });

        });
    }
  }
}
