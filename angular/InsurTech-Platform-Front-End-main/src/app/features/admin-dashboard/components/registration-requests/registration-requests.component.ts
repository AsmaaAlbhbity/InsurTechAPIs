import { ChangeDetectorRef, Component, NgModule, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { Config } from 'datatables.net-dt';
import 'datatables.net-responsive';
import Swal from 'sweetalert2';
import { Company } from '../../../../core/models/company';
import { appModuleAnimation } from '../../../../shared/animations/routerTransition';
import { data } from 'jquery';
import { RegistrationRequestsServiceService } from '../../../../core/services/registration-requests.service';
import { DataTablesModule } from 'angular-datatables';
import { HttpClientModule } from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faCheck, faEdit, faTimes, faUser, faX } from '@fortawesome/free-solid-svg-icons';
import { Subject } from 'rxjs';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';



@Component({
  selector: 'app-registration-requests',
  standalone: true,
  imports: [
    DataTablesModule,
    HttpClientModule,
    FontAwesomeModule,
    CommonModule,
    ButtonModule
  ],
  animations: [appModuleAnimation()],
  templateUrl: './registration-requests.component.html',
  styleUrl: './registration-requests.component.css'
})
export class RegistrationRequestsComponent {
faEdit = faEdit;
faCheck = faCheck;
faTimes = faTimes;
faUsers = faUser;
faX = faX;

  dtOptions: Config = {};
  companies: Company[] = [];
  newRequestCount: number = 0;
  approvedCount: number = 0;
  rejectedCount: number = 0;
  
  dtTrigger: Subject<any> = new Subject<any>();

constructor(
  private router: Router,
  private service : RegistrationRequestsServiceService,
  private cd: ChangeDetectorRef
) { }
  
ngOnInit(): void {
  this.dtOptions = {
    columnDefs: [
      { orderable: false, targets: -1 },
      { className: 'dt-left', targets: '_all' }
    ],
    processing: true,
    retrieve: true,
    responsive: true
  };
  
  this.loadData();
  }

  private loadData(): void {
    this.service.GetAll().subscribe((data: any) => {
      this.companies = data;
      this.updateCounts();
      this.cd.detectChanges();
      this.dtTrigger.next(null);
    });
  }

    private updateCounts(): void {
    this.newRequestCount = this.companies.filter(x => x.status === 'pending').length;
    this.approvedCount = this.companies.filter(x => x.status === 'approved').length;
    this.rejectedCount = this.companies.filter(x => x.status === 'rejected').length;
  }


  AcceptRequest(id: number){
    this.service.ApproveRequest(id).subscribe((data:any) => {
      const companyIndex = this.companies.findIndex(c => c.id === id);
      if (companyIndex > -1) {
        this.companies[companyIndex].status = 'approved';
        this.dtTrigger.next(null);
      }
    });
  }

  RejectRequest(id: number){
    this.service.RejectRequest(id).subscribe((data:any) => {
      const companyIndex = this.companies.findIndex(c => c.id === id);
      if (companyIndex > -1) {
        this.companies[companyIndex].status = 'rejected';
        this.dtTrigger.next(null);
      }
    });
  }

  SwitchRequestStatus(id: number){
    this.service.GetById(id).subscribe((data:any) => {
      if(data.status === "approved"){
        this.RejectRequest(id);
      } else {
        this.AcceptRequest(id);
      }
    });
  }

  showApproveAlert(id:number) {
    Swal.fire({
      title: "Approve Request",
      text: "Are you sure you want to approve this request?",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes"
    }).then((result) => {
      if (result.isConfirmed) {
        this.AcceptRequest(id);
        Swal.fire({
          title: "Approved!",
          text: "The request has been approved.",
          icon: "success"
        });
      }
    });
  }

  showDeclineAlert(id:number) {
    Swal.fire({
      title: "Decline Request",
      text: "Are you sure you want to decline this request?",
      icon: "error",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes"
    }).then((result) => {
      if (result.isConfirmed) {
        this.RejectRequest(id);
        Swal.fire({
          title: "Declined!",
          text: "The request has been declined.",
          icon: "success"
        });
      }
    });
  }

  showSwitchAlert(id:number) {
    Swal.fire({
      title: "Switch Request Status",
      text: "Are you sure you want to switch the status of this request?",
      icon: "info",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes"
    }).then((result) => {
      if (result.isConfirmed) {
        this.SwitchRequestStatus(id);
        Swal.fire({
          title: "Status Switched!",
          text: "The request status has been switched.",
          icon: "success"
        });
      }
    });
  }
}
