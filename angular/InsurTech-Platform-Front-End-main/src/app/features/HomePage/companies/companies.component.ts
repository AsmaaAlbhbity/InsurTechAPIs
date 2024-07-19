import { Component } from '@angular/core';
import { CarouselModule } from 'primeng/carousel';
import { ButtonModule } from 'primeng/button';
import { TagModule } from 'primeng/tag';
import { CommonModule } from '@angular/common';
import { CompaniesHomePage } from '../../../core/models/Home_Page/CompaniesHomePage';
import { GetCompaniesService } from '../../../core/services/HomePage/get-companies.service';

@Component({
  selector: 'app-companies',
  standalone: true,
  imports: [CommonModule, CarouselModule, ButtonModule, TagModule],
  templateUrl: './companies.component.html',
  styleUrl: './companies.component.css'
})
export class CompaniesComponent {
  companies: CompaniesHomePage[] | any;

  responsiveOptions: any[] | undefined;

  constructor(private companiesService: GetCompaniesService) {}

  ngOnInit() {
    this.companiesService.getCompanies().subscribe((companies) => {
      this.companies = companies;
      console.log(companies);
      console.log(this.companies);
    });

    this.responsiveOptions = [
      {
        breakpoint: '1199px',
        numVisible: 1,
        numScroll: 1
      },
      {
        breakpoint: '991px',
        numVisible: 2,
        numScroll: 1
      },
      {
        breakpoint: '767px',
        numVisible: 1,
        numScroll: 1
      }
    ];
  }

  getSeverity(insurancePlansCount: number) {
    if (insurancePlansCount > 50) {
      return 'success';
    } else if (insurancePlansCount > 10) {
      return 'warning';
    } else {
      return 'danger';
    }
  }
}
