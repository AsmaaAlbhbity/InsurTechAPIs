import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import{ MotorinsuranceService } from '../../../Services/ManageMotorServices/motorinsurance.service';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AddMotorInsurance, InsurancePlanLevel } from '../../../Model/Motorinsurance/add-motor-insurance';
import { ConfirmationService, MessageService } from 'primeng/api'
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';

import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-motorinsurance',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule,FormsModule,ToastModule,ConfirmDialogModule],
  providers: [MessageService, ConfirmationService],
  templateUrl: './motorinsurance.component.html',
  styleUrl: './motorinsurance.component.css'
})


export class MotorinsuranceComponent {
    InsurancePlanLevel = InsurancePlanLevel; 

   addmotorform:FormGroup=new FormGroup({
    yearlyCoverage: new FormControl(null, [Validators.required, Validators.min(0), Validators.max(1.7976931348623157e+308)]),
    level: new FormControl(this.InsurancePlanLevel.Basic, [Validators.required]),
    quotation: new FormControl(null, [Validators.required, Validators.min(0), Validators.max(1.7976931348623157e+308)]),
    personalAccident: new FormControl(null, [Validators.required, Validators.min(0), Validators.max(1.7976931348623157e+308)]),
    theft: new FormControl(null, [Validators.required, Validators.min(0), Validators.max(1.7976931348623157e+308)]),
    thirdPartyLiability: new FormControl(null, [Validators.required, Validators.min(0), Validators.max(1.7976931348623157e+308)]),
    ownDamage: new FormControl(null, [Validators.required, Validators.min(0), Validators.max(1.7976931348623157e+308)]),
    legalExpenses: new FormControl(null, [Validators.required, Validators.min(0), Validators.max(1.7976931348623157e+308)]),
    
  })
  userId: string = JSON.parse(localStorage.getItem('userData') || "{}").id;

  constructor(public motorservce:MotorinsuranceService,
    public route : ActivatedRoute, 
    private messageService: MessageService,
    ){}
  // userId: string = "57164a6c-e3b4-4ab5-8fd6-18fe3d29e68a"; 
  ngOnInit(): void {
    console.log('User ID:', this.userId); // This will log the user ID to the console

  }
  addmotor(){
    if(this.addmotorform.valid){
      const formValue = this.addmotorform.value;
      const newaddmotor:AddMotorInsurance=new AddMotorInsurance(
        formValue.yearlyCoverage,
        formValue.level,
        formValue.quotation,
        this.userId,
        formValue.personalAccident,
        formValue.theft,
        formValue.thirdPartyLiability,
        formValue.ownDamage,
        formValue.legalExpenses

      )
      this.motorservce.addmotor(newaddmotor).subscribe(
        response => {
          this.addmotorform.reset();
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



