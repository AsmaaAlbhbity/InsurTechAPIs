import { ChangeDetectorRef, Component, OnDestroy, OnInit, Renderer2 } from '@angular/core';
import { CompanyService } from '../../Services/CompanyServices/company.service';
import { Subscription } from 'rxjs';
import { ListInsurancePlan } from '../../Model/company/ListInsurancePlan'; // Add this import
import { HealthInsurancePlan } from '../../Model/company/HealthInsurancePlan';
import { HomeInsurancePlan } from '../../Model/company/HomeInsurancePlan';
import { MotorInsurancePlan } from '../../Model/company/MotorInsurancePlan';
import { ConfirmationService, MessageService } from 'primeng/api'
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
import { DropdownModule } from 'primeng/dropdown';
import { TagModule } from 'primeng/tag';
import { FormsModule } from '@angular/forms';
import { InputNumberModule } from 'primeng/inputnumber';
import { AvatarModule } from 'primeng/avatar';
import { MotorinsuranceComponent } from '../Motorinsurance/motorinsurance/motorinsurance.component';
import { HealthinsuranceComponent } from '../Helathinsurance/healthinsurance/healthinsurance.component';
import { HomeinsuranceComponent } from '../Homeinsurance/homeinsurance/homeinsurance.component';
import { EditHealthInsurancePlanComponent } from '../Helathinsurance/edithealthinsurance/edithealthinsurance.component';
import { EdithomeinsuranceComponent } from '../Homeinsurance/edithomeinsurance/edithomeinsurance.component';
import { EditmotorinsuranceComponent } from '../Motorinsurance/editmotorinsurance/editmotorinsurance.component';
import { EditMotorInsurance } from '../../Model/Motorinsurance/edit-motor-insurance';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-list-insurance-plan',
  standalone: true,
  imports: [CommonModule, TableModule, DialogModule, RippleModule, ButtonModule,
    ToastModule, ToolbarModule, ConfirmDialogModule, InputTextModule, InputTextareaModule, DropdownModule, TagModule,
    InputTextModule, InputNumberModule, FormsModule, RippleModule, AvatarModule, MotorinsuranceComponent,
    HealthinsuranceComponent, HomeinsuranceComponent, EditHealthInsurancePlanComponent, EdithomeinsuranceComponent,
    EditmotorinsuranceComponent, RouterLink],
  providers: [MessageService, ConfirmationService],
  templateUrl: './list-insurance-plan.component.html',
  styleUrls: ['./list-insurance-plan.component.css']
})
export class ListInsurancePlanComponent implements OnInit, OnDestroy {
  public HealthList: HealthInsurancePlan[] = [];
  public HomeList: HomeInsurancePlan[] = [];
  public MotorList: MotorInsurancePlan[] = [];
  visible: boolean = false;
  VisibleEdit: boolean = false;
  public flag: number = 1;

  private Id = JSON.parse(localStorage.getItem('userData') || '{id: 1}').id;
  subscriptions: Subscription[] = [];
  selectedInsurance: EditMotorInsurance = new EditMotorInsurance(0, 0, 0, 0, "", 0, 0, 0, 0, 0);
  dataToSend: any; // Define your dataToSend object if needed
  showEditModal = 0;

  ///
  constructor(
    private CompanyServices: CompanyService,
    private renderer: Renderer2,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.loadData();
    this.subscriptions.push(
      this.CompanyServices.insurancePlans$.subscribe({
        next: (data: ListInsurancePlan | null) => {
          if (data) {
            this.HealthList = data.healthInsurancePlans || [];
            this.HomeList = data.homeInsurancePlans || [];
            this.MotorList = data.motorInsurancePlans || [];
          }
        },
        error: (error: any) => { console.log(error); }
      })
    );
  }

  loadData() {
    this.CompanyServices.GetAll(this.Id);
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(sub => sub.unsubscribe());
  }

  click(name: string) {
    switch (name) {
      case "Motor": this.flag = 1; break;
      case "health": this.flag = 2; break;
      case "home": this.flag = 3; break;
    }
    var element = document.getElementById(name);
    if (element) {
      const elements = document.querySelectorAll(".col");
      elements.forEach(el => {
        this.renderer.removeStyle(el, 'background-color');
      });      
      this.renderer.setStyle(element, 'background-color', '#ffff');
    } else {
      console.error(`Element with id ${name} not found`);
    }
  }

  deleteInsurance(id: number) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete This plan?',
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      acceptButtonStyleClass: "p-button-danger p-button-text",
      rejectButtonStyleClass: "p-button-text p-button-text",
      acceptIcon: "none",
      rejectIcon: "none",
      accept: () => {
        this.CompanyServices.Delete(id).subscribe({
          next: (data: any) => {
            this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Insurance plan Deleted' });
            this.loadData();
          },
          error: (error: any) => {
            this.messageService.add({ severity: 'error', summary: 'Rejected', detail: 'An Error occurred' });
          }
        })
      },
      reject: () => {
        this.messageService.add({ severity: 'error', summary: 'Rejected', detail: 'You have rejected' });
      }
    });
  }
  showDialog() {
    this.visible = true;
  }
  // Function to show the modal with the selected component
  showModal(flag: number, data?: any) {

    debugger;
    if (data) {
      this.dataToSend = data;
      this.showEditModal = flag;
    }
    this.VisibleEdit = true; // Show the modal
  }

  // Function to hide the modal
  hideModal() {
    this.VisibleEdit = false; // Hide the modal
  }
}
