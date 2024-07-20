import { Component, OnDestroy,Input, OnInit, SimpleChanges, OnChanges } from '@angular/core';
import { EditHomeInsurance } from '../../../Model/Homeinsurance/edit-home-insurance';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import { HomeinsuranceService } from '../../../Services/ManageHomeServices/homeinsurance.service';
import { ActivatedRoute, Router } from '@angular/router';
import { InsurancePlanLevel } from '../../../Model/Homeinsurance/add-home-insurance';
import { ConfirmationService, MessageService } from 'primeng/api'
import { ToastModule } from 'primeng/toast';

@Component({
  selector: 'app-edithomeinsurance',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule,ToastModule],
  providers: [MessageService, ConfirmationService],
  templateUrl: './edithomeinsurance.component.html',
  styleUrls: ['./edithomeinsurance.component.css']
})
export class EdithomeinsuranceComponent implements OnChanges, OnDestroy {
  @Input() homeInsuranceData?: EditHomeInsurance;

  public EditObj: EditHomeInsurance = new EditHomeInsurance(0, 0, 0, 0, "", 0, 0, 0, 0, 0);
  Edithomeform!: FormGroup;
  sub!: Subscription;
  InsurancePlanLevel = InsurancePlanLevel;
  comanyId: string = JSON.parse(localStorage.getItem('userData') || "{}").id;

  constructor(
    private fb: FormBuilder,
    public homeservices: HomeinsuranceService,
    public activateRoute: ActivatedRoute,
    private messageService: MessageService,
    public router:Router) { }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['homeInsuranceData'] && this.homeInsuranceData) {
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
    this.Edithomeform= this.fb.group({
      yearlyCoverage: [this.homeInsuranceData?.yearlyCoverage || '', [Validators.required, Validators.min(0), Validators.max(Number.MAX_SAFE_INTEGER)]],
      level: [this.homeInsuranceData?.level || InsurancePlanLevel.Basic, [Validators.required]],
      quotation: [this.homeInsuranceData?.quotation || '', [Validators.required, Validators.min(0), Validators.max(Number.MAX_SAFE_INTEGER)]],
      waterDamage: [this.homeInsuranceData?.waterDamage || '', [Validators.required, Validators.min(0), Validators.max(Number.MAX_SAFE_INTEGER)]],
      glassBreakage: [this.homeInsuranceData?.glassBreakage || '', [Validators.required, Validators.min(0), Validators.max(Number.MAX_SAFE_INTEGER)]],
      naturalHazard: [this.homeInsuranceData?.naturalHazard || '', [Validators.required, Validators.min(0), Validators.max(Number.MAX_SAFE_INTEGER)]],
      attemptedTheft: [this.homeInsuranceData?.attemptedTheft || '', [Validators.required, Validators.min(0), Validators.max(Number.MAX_SAFE_INTEGER)]],
      firesAndExplosion: [this.homeInsuranceData?.firesAndExplosion || '', [Validators.required, Validators.min(0), Validators.max(Number.MAX_SAFE_INTEGER)]]
    });
    
  }

  EditHealth(): void {
    debugger;
    if (this.Edithomeform.valid) {
      const formValue = this.Edithomeform.value;
      const homeObj: EditHomeInsurance = new EditHomeInsurance(
        this.homeInsuranceData?.id||0,
        formValue.yearlyCoverage,
        formValue.level,
        formValue.quotation,
        this.comanyId,
        formValue.waterDamage,
        formValue.glassBreakage,
        formValue.naturalHazard,
        formValue.attemptedTheft,
        formValue.firesAndExplosion
      );

      console.log('Payload:', homeObj); // Log the payload to verify
      this.homeservices.edit(homeObj).subscribe(
        {
          next: (data) => {
            this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Insurance plan Has Been Editied' });    

          },
          error: (error) => {
            this.messageService.add({ severity: 'error', summary: 'Rejected', detail: 'Error Occured Try again later' });
          }
        }
      );
    } else {
      console.error('Form is invalid');
    }
  }
}

