import { Component, OnInit } from '@angular/core';
import { Categories } from '../../../core/models/Home_Page/Categories';
import { GetCategoriesService } from '../../../core/services/HomePage/get-categories.service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [CommonModule,RouterLink],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.css'
})
export class CategoriesComponent implements OnInit {

  categories: Categories[]=[];

  constructor(private categoriesService: GetCategoriesService) { }

  ngOnInit(): void {
    this.fetchCategories();
  }

  fetchCategories(): void {
    this.categoriesService.getCategories().subscribe(
      (data) => {
        this.categories = data;
        console.log(this.categories);
        
      },
      (error) => {
        console.error('Error fetching categories: ', error);
      }
    );
  }

  getCardColor(index: number): string {
      const colors = ['blue', 'green', 'yellow', 'brown', 'purple', 'orange'];
    return colors[index % colors.length]; // Cycle through colors based on index
  }

}