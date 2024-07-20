import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { EditMotorInsurance } from '../../../Model/Motorinsurance/edit-motor-insurance';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { MotorinsuranceService } from '../../../Services/ManageMotorServices/motorinsurance.service';
import { ActivatedRoute, Router } from '@angular/router';
import { InsurancePlanLevel } from '../../../Model/Motorinsurance/add-motor-insurance';
import { ConfirmationService, MessageService } from 'primeng/api'
import { ToastModule } from 'primeng/toast';


@Component({
  selector: 'app-editmotorinsurance',
  standalone: true,
  providers: [MessageService, ConfirmationService],
  imports: [ReactiveFormsModule, CommonModule,ToastModule],
  templateUrl: './editmotorinsurance.component.html',
  styleUrl: './editmotorinsurance.component.css'
})
export class EditmotorinsuranceComponent implements  OnChanges, OnDestroy {
  @Input() motorInsuranceData?: EditMotorInsurance;
  Editmotorform!: FormGroup;
  sub!: Subscription;
  InsurancePlanLevel = InsurancePlanLevel;
  comanyId: string = JSON.parse(localStorage.getItem('userData') || "{}").id;

  constructor(
    private fb: FormBuilder,
    private messageService: MessageService,
    private motorservices: MotorinsuranceService,
    private activateRoute: ActivatedRoute,
    private router: Router) { }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['motorInsuranceData'] && this.motorInsuranceData) {
      this.initializeForm();
    }
  }

  initializeForm(): void {
    this.Editmotorform = this.fb.group({
      yearlyCoverage: [this.motorInsuranceData?.yearlyCoverage || '', [Validators.required, Validators.min(0), Validators.max(Number.MAX_SAFE_INTEGER)]],
      level: [this.motorInsuranceData?.level || InsurancePlanLevel.Basic, [Validators.required]],
      quotation: [this.motorInsuranceData?.quotation || '', [Validators.required, Validators.min(0), Validators.max(Number.MAX_SAFE_INTEGER)]],
      personalAccident: [this.motorInsuranceData?.personalAccident || '', [Validators.required, Validators.min(0), Validators.max(Number.MAX_SAFE_INTEGER)]],
      theft: [this.motorInsuranceData?.theft || '', [Validators.required, Validators.min(0), Validators.max(Number.MAX_SAFE_INTEGER)]],
      thirdPartyLiability: [this.motorInsuranceData?.thirdPartyLiability || '', [Validators.required, Validators.min(0), Validators.max(Number.MAX_SAFE_INTEGER)]],
      ownDamage: [this.motorInsuranceData?.ownDamage || '', [Validators.required, Validators.min(0), Validators.max(Number.MAX_SAFE_INTEGER)]],
      legalExpenses: [this.motorInsuranceData?.legalExpenses || '', [Validators.required, Validators.min(0), Validators.max(Number.MAX_SAFE_INTEGER)]]
    });
  }

  ngOnDestroy(): void {
    if (this.sub) {
      this.sub.unsubscribe();
    }
  }

  EditMotor(): void {
    debugger;
    if (this.Editmotorform.valid) {
      const formValue = this.Editmotorform.value;
      const homeObj: EditMotorInsurance = new EditMotorInsurance(
        this.motorInsuranceData?.id || 0,
        formValue.yearlyCoverage,
        formValue.level,
        formValue.quotation,
        this.comanyId,
        formValue.personalAccident,
        formValue.theft,
        formValue.thirdPartyLiability,
        formValue.ownDamage,
        formValue.legalExpenses
      );

      this.motorservices.edit(homeObj).subscribe(
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