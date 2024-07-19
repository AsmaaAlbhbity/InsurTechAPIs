import { Component, OnInit } from '@angular/core';
import { ChartOptions } from 'chart.js';
import { PrimeNGConfig } from 'primeng/api';
import { InsuranceChartService } from '../../../../../core/services/insurance-chart.service';
import { ChartModule } from 'primeng/chart';

@Component({
  selector: 'Insurance-chart',
  templateUrl: './insurance-chart.component.html',
  styleUrls: ['./insurance-chart.component.css'],
  standalone: true,
  imports: [ChartModule]
})
export class InsuranceChartComponent implements OnInit {
  data: any;
  options!: ChartOptions<'doughnut'>;

  constructor(private insuranceService: InsuranceChartService, private primengConfig: PrimeNGConfig) {}

  ngOnInit() {
    this.primengConfig.setTranslation({
     
    });

    this.insuranceService.getAllData().subscribe({
      next: (data: any[]) => {
        if(data[1] === "No Insurances Yet"){
          data[1] = [];
        }
        this.processData(data);
      },
      error: (error) => {
        console.error('Error fetching insurance data:', error);
      }
    });

    
  }

  processData(apiResponses: any[]) {
    const healthInsuranceCount = apiResponses[0].length;
    const homeInsuranceCount = apiResponses[1].length;
    const motorInsuranceCount = apiResponses[2].length;

    const labels = ['Health Insurance', 'Home Insurance', 'Motor Insurance'];

    this.data = {
      labels: labels,
      datasets: [
        {
          data: [healthInsuranceCount, homeInsuranceCount, motorInsuranceCount],
          backgroundColor: ['#007BFF', '#FFC107', '#28A745'],
          hoverBackgroundColor: ['#0056b3', '#FFA000', '#218838']
        }
      ]
    };

    this.options = {
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        legend: {
          position: 'bottom'
        },
        tooltip: {
          callbacks: {
            label: (context) => {
              const label = context.label || '';
              const value = context.raw;
              return `${label}: ${value}`;
            }
          }
        }
      }
    };
  }
}
