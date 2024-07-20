import { Component, OnInit } from '@angular/core';
import { UserService } from '../../layout/service/crud-user.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { PaginatorModule } from 'primeng/paginator';
import { User, Company } from './iuserType'; // Import the user types
import { ConfirmationService, MessageService } from 'primeng/api';
import { MessagesModule } from 'primeng/messages';
import { ToastModule } from 'primeng/toast'; // Import ToastModule
import { DialogModule } from 'primeng/dialog';
import { RippleModule } from 'primeng/ripple';
import { ToolbarModule } from 'primeng/toolbar';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { DropdownModule } from 'primeng/dropdown';

@Component({
  selector: 'app-admin-crud-users',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    NgxPaginationModule,
    TableModule,
    ButtonModule,
    InputTextModule,
    PaginatorModule,
    MessagesModule,
    ToastModule,
    DialogModule,
    RippleModule,
    ToolbarModule,
    ConfirmDialogModule,
    InputTextareaModule,
    DropdownModule,
  ],
  providers: [ConfirmationService, MessageService],
  templateUrl: './admin-crud-users.component.html',
  styleUrls: ['./admin-crud-users.component.css'],
})
export class AdminCrudUsersComponent implements OnInit {
  users: User[] = [];
  filteredUsers: User[] = [];
  searchTerm: string = '';
  currentPage: number = 1;
  activeTab: string = 'customers'; // Default active tab

  displayEditDialog: boolean = false;
  displayViewDialog: boolean = false;
  selectedUser: User | null = null;
  editUserModel: User | Company | null = null;

  constructor(
    private userService: UserService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit() {
    const observer = {
      next: (data: User[]) => {
        this.users = data;
        this.filterUsers(); // Initialize filteredUsers
      },
      error: (error: any) => {
        console.error('Error fetching users:', error);
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Failed to fetch users.',
        });
      },
    };

    this.userService.getUsers().subscribe(observer);
  }

  getUserType(userType: number) {
    return userType === 1 ? 'company' : 'customer';
  }

  filterUsers() {
    let filtered = this.users;
    if (this.searchTerm) {
      filtered = filtered.filter(
        (user) =>
          user.name.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
          user.userName.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
          user.email.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
          user.phoneNumber.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    }

    this.filteredUsers = filtered.filter((user) =>
      this.activeTab === 'customers' ? user.userType === 0 : user.userType === 1
    );
  }

  setActiveTab(tab: string) {
    this.activeTab = tab;
    this.filterUsers();
  }

  editUser(user: User) {
    this.editUserModel = { ...user };
    this.displayEditDialog = true;
  }

  viewUser(user: User) {
    this.selectedUser = user;
    this.displayViewDialog = true;
  }

  get editUserTaxNumber(): string {
    return (this.editUserModel as Company)?.taxNumber || '';
  }

  set editUserTaxNumber(value: string) {
    if (this.editUserModel && this.editUserModel.userType === 1) {
      (this.editUserModel as Company).taxNumber = value;
    }
  }

  get editUserLocation(): string {
    return (this.editUserModel as Company)?.location || '';
  }

  set editUserLocation(value: string) {
    if (this.editUserModel && this.editUserModel.userType === 1) {
      (this.editUserModel as Company).location = value;
    }
  }

  saveUser() {
    if (this.editUserModel) {
      this.confirmationService.confirm({
        message: `Are you sure you want to edit user with ID: ${this.editUserModel.id}?`,
        header: 'Edit Confirmation',
        icon: 'pi pi-info-circle',
        acceptButtonStyleClass: 'p-button-warning p-button-text',
        rejectButtonStyleClass: 'p-button-text p-button-text',
        acceptIcon: 'none',
        rejectIcon: 'none',
        accept: () => {
          const observer = {
            next: () => {
              const index = this.users.findIndex(
                (u) => u.id === this.editUserModel!.id
              );
              if (index !== -1) {
                this.users[index] = this.editUserModel!;
                this.filterUsers();
                this.messageService.add({
                  severity: 'success',
                  summary: 'Successful',
                  detail: 'User updated successfully',
                });
              }
              this.displayEditDialog = false;
            },
            error: (error: any) => {
              console.error('Error updating user:', error);
              this.messageService.add({
                severity: 'error',
                summary: 'Error',
                detail: 'There was an error updating the user.',
              });
            },
          };
          this.userService
            .editUser(this.editUserModel as User, this.editUserModel!.userType)
            .subscribe(observer);
        },
        reject: () => {
          this.messageService.add({
            severity: 'info',
            summary: 'Cancelled',
            detail: 'User edit cancelled',
          });
        },
      });
    }
  }

  deleteUser(user: User) {
    this.confirmationService.confirm({
      message: `Are you sure you want to delete user with ID: ${user.id}?`,
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      acceptButtonStyleClass: 'p-button-danger p-button-text',
      rejectButtonStyleClass: 'p-button-text p-button-text',
      acceptIcon: 'none',
      rejectIcon: 'none',
      accept: () => {
        const observer = {
          next: () => {
            this.users = this.users.filter((u) => u.id !== user.id);
            this.filterUsers();
            this.messageService.add({
              severity: 'success',
              summary: 'Successful',
              detail: 'User Deleted',
            });
          },
          error: (error: any) => {
            console.error('Error deleting user:', error);
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: 'There was an error deleting the user.',
            });
          },
        };

        this.userService.deleteUser(user.id, user.userType).subscribe(observer);
      },
      reject: () => {
        this.messageService.add({
          severity: 'info',
          summary: 'Cancelled',
          detail: 'Your user is safe',
        });
      },
    });
  }
}
