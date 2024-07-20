import { Component, OnInit } from '@angular/core';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router'; 
import { CategoriesService } from '../../../core/services/categories.service';
import { HeaderComponent } from '../../../shared/components/header/header.component';
import { BASE_URL } from '../../../core/base-url';

@Component({
  selector: 'Healthcategory',
  standalone: true,
  imports: [TableModule, ButtonModule, CommonModule, FormsModule,HeaderComponent],
  templateUrl: './sub-category.component.html',
  styleUrls: ['./sub-category.component.css'], 
})
export class SubCategoryHealthComponent implements OnInit {
  data: any[] = [];
  loading: boolean = false;

  constructor(private router: Router, private categoriesService: CategoriesService) {}

  ngOnInit() {
    this.fetchData();
  }

  fetchData() {
    this.loading = true;
    this.categoriesService.getCategories(`${BASE_URL}/HealthInsurance/GetHealthInsurance`).subscribe(
      (data) => {
        this.data = data;
        this.loading = false;
      },
      (error) => {
        console.error('Error fetching data', error);
        this.loading = false;
      }
    );
  }

  goBack(): void {
    this.router.navigate(['/insurance']);
  }
}
