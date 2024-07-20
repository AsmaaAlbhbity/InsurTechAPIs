import { Component, OnInit, OnDestroy, Input, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HealthinsuranceService } from '../../../Services/ManageHealthServices/healthinsurance.service';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { EditHealthInsurance, InsurancePlanLevel } from '../../../Model/Helathinsurance/edit-health-insurance';
import { ConfirmationService, MessageService } from 'primeng/api'
import { ToastModule } from 'primeng/toast';

@Component({
  selector: 'app-edit-health-insurance-plan',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule,ToastModule],
  providers: [MessageService, ConfirmationService],
  templateUrl: './edithealthinsurance.component.html',
  styleUrls: ['./edithealthinsurance.component.css']
})
export class EditHealthInsurancePlanComponent implements OnChanges, OnDestroy {
  @Input() healthInsuranceData?: EditHealthInsurance;

  public EditObj: EditHealthInsurance = new EditHealthInsurance(0,0, 0, 0, "", "", 0, 0, 0, 0);
  Edithealthform!: FormGroup;
  sub!: Subscription;
  InsurancePlanLevel = InsurancePlanLevel; 
  comanyId: string = JSON.parse(localStorage.getItem('userData') || "{}").id;

  constructor(    
    private fb: FormBuilder,
    public healthService: HealthinsuranceService,
    public activateRoute: ActivatedRoute,
    private messageService: MessageService,
  public router:Router) {}


  ngOnChanges(changes: SimpleChanges): void {
    if (changes['healthInsuranceData'] && this.healthInsuranceData) {
      this.initializeForm();
    }
  }
  ngOnDestroy(): void {
    if (this.sub) {
      this.sub.unsubscribe();
    }
  }

  initializeForm(): void {
    // Patch the form controls with the data
    this.Edithealthform=this.fb.group({
      yearlyCoverage: [this.healthInsuranceData?.yearlyCoverage || '', [Validators.required, Validators.min(0), Validators.max(Number.MAX_SAFE_INTEGER)]],
      level: [this.healthInsuranceData?.level || InsurancePlanLevel.Basic, [Validators.required]],
      quotation: [this.healthInsuranceData?.quotation || '', [Validators.required, Validators.min(0), Validators.max(Number.MAX_SAFE_INTEGER)]],
      medicalNetwork: [this.healthInsuranceData?.medicalNetwork || '', [Validators.required, Validators.min(0), Validators.max(Number.MAX_SAFE_INTEGER)]],
      clinicsCoverage: [this.healthInsuranceData?.clinicsCoverage || '', [Validators.required, Validators.min(0), Validators.max(Number.MAX_SAFE_INTEGER)]],
      hospitalizationAndSurgery: [this.healthInsuranceData?.hospitalizationAndSurgery || '', [Validators.required, Validators.min(0), Validators.max(Number.MAX_SAFE_INTEGER)]],
      opticalCoverage: [this.healthInsuranceData?.opticalCoverage || '', [Validators.required, Validators.min(0), Validators.max(Number.MAX_SAFE_INTEGER)]],
      dentalCoverage: [this.healthInsuranceData?.dentalCoverage || '', [Validators.required, Validators.min(0), Validators.max(Number.MAX_SAFE_INTEGER)]]
    });

   
  }

  EditHealth() {
    if (this.Edithealthform.valid) {
      debugger;

      const formValue = this.Edithealthform.value;
      const healthObj: EditHealthInsurance = new EditHealthInsurance(
        this.healthInsuranceData?.id ||0,
        formValue.yearlyCoverage,
        formValue.level,
        formValue.quotation,
        this.comanyId,
        formValue.medicalNetwork,
        formValue.clinicsCoverage,
        formValue.hospitalizationAndSurgery,
        formValue.opticalCoverage,
        formValue.dentalCoverage
      );

      console.log('Payload:', healthObj); // Log the payload to verify
      this.healthService.edit(healthObj).subscribe(
        {
          next:(data) => {
            this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Insurance plan Has Been Editied' });    
          },
          error:(error) => {
            this.messageService.add({ severity: 'error', summary: 'Rejected', detail: 'Error Occured Try again later' });
          }
        }
      );
    } else {
      console.error('Form is invalid');
    }
  }
}


