import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ChartModule } from 'primeng/chart';
import { InsuranceChartService } from '../../../../core/services/insurance-chart.service';
import { InsuranceChartComponent } from './insurance-chart/insurance-chart.component';
import { UsersChartComponent } from './users-chart/users-chart.component';
import { InsuranceRevenueChartComponent } from './insurance-revenue-chart/insurance-revenue-chart.component';

@Component({
    selector: 'admin-graphs',
    templateUrl: './admin-graphs.component.html',
    standalone: true,
    imports: [ChartModule,HttpClientModule,InsuranceChartComponent,UsersChartComponent,InsuranceRevenueChartComponent]
})
export class ChartPieDemo  {
    // data: any;
    // options: any;
    // constructor(private insuranceService:InsuranceChartService){}

    // ngOnInit() {
    //     const documentStyle = getComputedStyle(document.documentElement);
    //     const textColor = documentStyle.getPropertyValue('--text-color');

    //     this.options = {
    //         responsive: true,
    //         maintainAspectRatio: false,
    //         plugins: {
    //             legend: {
    //                 labels: {
    //                     usePointStyle: true,
    //                     color: textColor
    //                 }
    //             }
    //         }
    //     };

    //     this.insuranceService.getAllData().subscribe(data => {
    //         this.processData(data);});
    // }
    // processData(apiResponses: any[]) {
    //     const counts = apiResponses.map(response => response.length);
    //     const labels = ['Health Insurance', 'Home Insurance', 'Motor Insurance'];
    
    //     const documentStyle = getComputedStyle(document.documentElement);
    
    //     this.data = {
    //       labels: labels,
    //       datasets: [
    //         {
    //           data: counts,
    //           backgroundColor: [
    //             documentStyle.getPropertyValue('--blue-500'),
    //             documentStyle.getPropertyValue('--yellow-500'),
    //             documentStyle.getPropertyValue('--green-500')
    //           ],
    //           hoverBackgroundColor: [
    //             documentStyle.getPropertyValue('--blue-400'),
    //             documentStyle.getPropertyValue('--yellow-400'),
    //             documentStyle.getPropertyValue('--green-400')
    //           ]
    //         }
    //       ]
    //     };
    //   }
}
