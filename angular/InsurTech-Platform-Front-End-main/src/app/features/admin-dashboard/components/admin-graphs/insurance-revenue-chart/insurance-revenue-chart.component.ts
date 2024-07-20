import { Component,OnInit,inject } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { ChartModule } from 'primeng/chart';
import { InsuranceChartService } from '../../../../../core/services/insurance-chart.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-insurance-revenue-chart',
  standalone: true,
  imports: [ChartModule,HttpClientModule,CommonModule],
  templateUrl: './insurance-revenue-chart.component.html',
  styleUrl: './insurance-revenue-chart.component.css',
  providers: [InsuranceChartService]
})
export class InsuranceRevenueChartComponent implements OnInit {
  data: any;
  options: any;
  constructor(private insuranceService:InsuranceChartService){}
  ngOnInit() {
    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');
    const surfaceBorder = documentStyle.getPropertyValue('--surface-border');

    this.options = {
      responsive: true,
      plugins: {
          legend: {
              labels: {
                  color: textColor
              }
          }
      },
      scales: {
          r: {
              grid: {
                  color: surfaceBorder
              }
          }
      }
  };

  this.fetchData(documentStyle);
}
fetchData(documentStyle: CSSStyleDeclaration) {
  this.insuranceService.getMotorInsurance().subscribe(motorData => {
      this.insuranceService.getHealthInsurance().subscribe(healthData => {
          this.insuranceService.getHomeInsurance().subscribe(homeData => {
              const totalMotorCoverage = this.aggregateCoverage(motorData);
              const totalHealthCoverage = this.aggregateCoverage(healthData);
              const totalHomeCoverage = this.aggregateCoverage(homeData);

              this.data = {
                  datasets: [
                      {
                          data: [totalMotorCoverage, totalHealthCoverage, totalHomeCoverage],
                          backgroundColor: [
                              documentStyle.getPropertyValue('--red-500'),
                              documentStyle.getPropertyValue('--green-500'),
                              documentStyle.getPropertyValue('--yellow-500')
                          ],
                          label: 'Yearly Coverage'
                      }
                  ],
                  labels: ['Motor Insurance Revenue', 'Health Insurance Revenue', 'Home Insurance Revenue']
              };
          });
      });
  });
}
aggregateCoverage(data: any[]): number {
  return data.reduce((sum, item) => sum + item.yearlyCoverage, 0);
}

}
