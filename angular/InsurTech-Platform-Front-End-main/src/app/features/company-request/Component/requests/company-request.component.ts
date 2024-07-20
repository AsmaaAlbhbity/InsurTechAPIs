
import { Component, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';
import { RippleModule } from 'primeng/ripple';
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { CommonModule } from '@angular/common';
import { FileUploadModule } from 'primeng/fileupload';
import { DropdownModule } from 'primeng/dropdown';
import { TagModule } from 'primeng/tag';
import { RadioButtonModule } from 'primeng/radiobutton';
import { RatingModule } from 'primeng/rating';
import { FormsModule } from '@angular/forms';
import { InputNumberModule } from 'primeng/inputnumber';
import { ChangeDetectorRef } from '@angular/core';
import { StatusLabelPipe } from '../../../../pipes/status-label.pipe';
import { LevelInsurancePipe } from '../../../../pipes/level-insurance.pipe';
import { CompanyRequests, InsurancePlanLevel, RequestStatus } from '../../Models/company-requests';
import { CompanyRequestsService } from '../../Services/company-requests.service';
import { SidenavComponent } from '../../../company-dashboard/Componants/sidenav/sidenav.component';


@Component({
  selector: 'app-company-request',
  standalone: true,
  imports: [TableModule, DialogModule,SidenavComponent, RippleModule, StatusLabelPipe, LevelInsurancePipe, ButtonModule, ToastModule, ToolbarModule, ConfirmDialogModule, InputTextModule, InputTextareaModule, CommonModule, FileUploadModule, DropdownModule, TagModule, RadioButtonModule, RatingModule, InputTextModule, FormsModule, InputNumberModule],
  providers: [MessageService, ConfirmationService],
  styles: [
    `:host ::ng-deep .p-dialog .product-image {
          width: 150px;
          margin: 0 auto 2rem auto;
          display: block;
      }`
  ],
  templateUrl: './company-request.component.html',
  styleUrl: './company-request.component.css'
})
export class CompanyRequestComponent implements OnInit {
  

  private Id =JSON.parse(localStorage.getItem('userData') || '{id: 1}').id;  

  productDialog: boolean = false;
  companyRequsts!: CompanyRequests[];
  request!: CompanyRequests;
  submitted: boolean = false;

  statuses!: any[];
  req!: {};
  requestQuestions!: any[];
  currentRequest: CompanyRequests | undefined;
  cntPending:number=0;
  cntRejected:number=0;
  cntAccepted:number=0;

  constructor(private companyRequestsService: CompanyRequestsService, private messageService: MessageService, private confirmationService: ConfirmationService, private changeDetectorRef: ChangeDetectorRef) { }

  ngOnInit() {
    debugger;
    this.companyRequestsService.GetAllRequestsByCompanyId(this.Id)
      .subscribe((request) => {
        this.companyRequsts = request;
        this.companyRequsts .forEach(r => {
          if(r.status==0)
            this.cntPending+=1;
          if(r.status==1)
            this.cntAccepted+=1;
          if(r.status==2)
            this.cntRejected+=1;
     

        });


      });
      console.log(this.companyRequsts);
    this.statuses = [
      { label: 'Pending', value: 'Pending' },
      { label: 'Approved', value: 'Approved' },
      { label: 'Rejected', value: 'Rejected' },
    ];
  }

  openNew() {
    this.req = {};
    this.submitted = false;
    this.productDialog = true;
  }


  viewRequsts(requestQuestions: any[]) {
    debugger;
    // Make a deep copy of the array to avoid modifying the original data
    this.requestQuestions = JSON.parse(JSON.stringify(requestQuestions));

    console.log(this.requestQuestions); // Log to verify data

    // Ensure the data is properly updated
    this.changeDetectorRef.detectChanges();

    // Open the dialog
    this.productDialog = true;
  }

  approveRequest(requestId: string) {
    // var currentRequest: CompanyRequests | undefined;
    this.confirmationService.confirm({
      message: 'Are you sure you want to Approve  this Request ?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        // Assuming this.companyRequests is an array of CompanyRequest
        this.currentRequest = this.companyRequsts.find((val) => val.id === requestId);

        if (this.currentRequest) {
          debugger;
          this.currentRequest.status=RequestStatus.Approved;
          this.companyRequestsService.UpdateAndNotify(this.currentRequest.id,this.currentRequest)
          .subscribe(
            response => {
              console.log('Request updated successfully:');
             
            },
            error => {
              console.error('Error updating request:', error);
            });
        

        } else {
          console.log('Request not found');
        } 
      
        this.req = {};
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Request Approved', life: 3000 });
      },
      reject: () => {
        this.messageService.add({ 
          severity: 'danger', 
          summary: 'Request Canceled', 
          detail: 'The request has been canceled.', 
          life: 3000 
      });      
  }      
    });
  }
  rejectRequest(requestId: string){
    this.confirmationService.confirm({
      message: 'Are you sure you want to Reject  this Request ?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        // Assuming this.companyRequests is an array of CompanyRequest
        this.currentRequest = this.companyRequsts.find((val) => val.id === requestId);

        if (this.currentRequest) {
          console.log(this.currentRequest.status);
          this.currentRequest.status=RequestStatus.Rejected;
          this.companyRequestsService.UpdateAndNotify(this.currentRequest.id,this.currentRequest)
          .subscribe(
            response => {
              console.log('Request updated successfully:');
             
            },
            error => {
              console.error('Error updating request:', error);
            });

          console.log(this.currentRequest.status);

        } else {
          console.log('Request not found');
        } 
      
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Request Rejected', life: 3000 });
      },
      reject: () => {
        this.messageService.add({ 
          severity: 'danger', 
          summary: 'Request Canceled', 
          detail: 'The request has been canceled.', 
          life: 3000 
      });      
  }      
    });
  }
  hideDialog() {
    this.productDialog = false;
    this.submitted = false;
  }

  findIndexById(id: string): number {
    let index = -1;
    for (let i = 0; i < this.companyRequsts.length; i++) {
      if (this.companyRequsts[i].id === id) {
        index = i;
        break;
      }
    }

    return index;
  }

  getSeverity(status: RequestStatus): '#20a76b' | '#FF9800' | '#e90000' | '' {
    switch (status) {
      case RequestStatus.Approved:
        return '#20a76b';
      case RequestStatus.Pending:
        return '#FF9800';
      case RequestStatus.Rejected:
        return '#e90000';
      default:
        return '';
    }
  }
  getSeverityLevel(level: InsurancePlanLevel): "info" | "success" | "warning" | undefined {
    switch (level) {
      case InsurancePlanLevel.basic:
        return 'info';
      case InsurancePlanLevel.Standard:
        return 'success';
      case InsurancePlanLevel.Premium:
        return 'warning';
      default:
        return undefined;
    }
  }
}