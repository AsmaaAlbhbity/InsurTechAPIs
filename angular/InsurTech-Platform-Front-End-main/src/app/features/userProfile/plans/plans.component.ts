// src/app/plans/plans.component.ts
import { Component, OnInit } from '@angular/core';
import { getRequsestsService } from "../../../core/services/userProfile/getRequests.services"
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-plans',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './plans.component.html',
  styleUrls: ['./plans.component.css']
})
export class PlansComponent implements OnInit {
  pendingRequests: any[] = [];
  paidRequests: any[] = [];
  approvedButNotPaidRequests: any[] = [];
  rejectedRequests: any[] = [];

  constructor(private getRequsestsService: getRequsestsService, private router: Router) {}

  ngOnInit(): void {
    this.loadPendingRequests();
    this.loadPaidRequests();
    this.loadApprovedButNotPaidRequests();
    this.loadRejectedRequests();
    console.log(this.pendingRequests);
  }

  loadPendingRequests(): void {
    this.getRequsestsService.getUserRequestsWithPendingStatus().subscribe({
      next: (data) => this.pendingRequests = data,
      error: (err) => console.error('Error loading pending requests', err)
    });
  }

  loadPaidRequests(): void {
    this.getRequsestsService.getRequestsWithPaidTrue().subscribe({
      next: (data) => {this.paidRequests = data},
      error: (err) => console.error('Error loading paid requests', err)
    });
  }

  loadApprovedButNotPaidRequests(): void {
    this.getRequsestsService.getRequestsWithApprovedStatusButNotPaid().subscribe({
      next: (data) => {this.approvedButNotPaidRequests = data, console.log(data)},
      error: (err) => console.error('Error loading approved but not paid requests', err)
    });
    console.log(this.approvedButNotPaidRequests);
  }

  loadRejectedRequests(): void {
    this.getRequsestsService.getRequestsWithRejectedStatus().subscribe({
      next: (data) => this.rejectedRequests = data,
      error: (err) => console.error('Error loading rejected requests', err)
    });
  }
  Payment(plan: any): void{
     this.router.navigate(['payment'], { state: { plan: plan } });
  }
  CancelSubscription(plan: any): void {
    Swal.fire({
      title: 'Are you sure?',
      text: 'Do you really want to cancel this subscription?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, cancel it!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.getRequsestsService.changePaidToFalse(plan.planId).subscribe({
          next: (data) => {
            this.loadApprovedButNotPaidRequests();
            this.loadPaidRequests();
            console.log(data);
            Swal.fire(
              'Cancelled!',
              'Your subscription has been cancelled.',
              'success'
            );
          },
          error: (err) => console.error('Error loading approved but not paid requests', err)
        });
      }
    });
  }
  
  
}
