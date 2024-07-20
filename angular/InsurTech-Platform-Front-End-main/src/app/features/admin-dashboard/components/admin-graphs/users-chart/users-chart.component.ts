import { Component, OnInit } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';
import { UserChartService } from '../../../../../core/services/user-chart.service';
import { ChartModule } from 'primeng/chart';

@Component({
  selector: 'app-users-chart',
  standalone: true,
  imports: [ChartModule],
  templateUrl: './users-chart.component.html',
  styleUrls: ['./users-chart.component.css']
})
export class UsersChartComponent implements OnInit {

  basicData: any;
  basicOptions: any;

  constructor(private userChartService: UserChartService, private primengConfig: PrimeNGConfig) { }

  ngOnInit() {
    this.loadChartData();
    this.setChartOptions();
  }

  private loadChartData() {
    let companiesCount = 0;
    let customersCount = 0;

    this.userChartService.getCompanies().subscribe({
      next: (data: any[]) => {
        if (data[1] === "No Companies Yet") {
          data[1] = [];
        }
        companiesCount = data.length;
        this.updateChartData(companiesCount, customersCount);
      },
      error: (error) => {
        console.error('Error fetching companies data:', error);
      }
    });

    this.userChartService.getUsers().subscribe({
      next: (data: any[]) => {
        if (data[1] === "No Users Yet") {
          data[1] = [];
        }
        customersCount = data.filter(user => user.userType === 1).length;
        this.updateChartData(companiesCount, customersCount);
      },
      error: (error) => {
        console.error('Error fetching users data:', error);
      }
    });
  }

  private updateChartData(companiesCount: number, customersCount: number) {
    console.log(companiesCount, customersCount);
    this.basicData = {
      labels: ['Companies', 'Customers'],
      datasets: [
        {
          label: 'Users',
          data: [companiesCount, customersCount],
          backgroundColor: ['rgba(255, 159, 64, 0.2)', 'rgba(75, 192, 192, 0.2)'],
          borderColor: ['rgb(255, 159, 64)', 'rgb(75, 192, 192)'],
          borderWidth: 1
        }
      ]
    };
  }

  private setChartOptions() {
    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');
    const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
    const surfaceBorder = documentStyle.getPropertyValue('--surface-border');

    this.basicOptions = {
      responsive: true,
      plugins: {
        legend: {
          labels: {
            color: textColor
          }
        }
      },
      scales: {
        y: {
          beginAtZero: true,
          ticks: {
            color: textColorSecondary
          },
          grid: {
            color: surfaceBorder,
            drawBorder: false
          }
        },
        x: {
          ticks: {
            color: textColorSecondary
          },
          grid: {
            color: surfaceBorder,
            drawBorder: false
          }
        }
      }
    };
  }
}
