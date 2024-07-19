import { Component } from '@angular/core';
import { HeaderComponent } from '../../shared/components/header/header.component';
import { FooterComponent } from '../../shared/components/footer/footer.component';
import { RouterLink } from '@angular/router';
import { ImgHeaderComponent } from '../HomePage/img-header/img-header.component';
import { CategoriesComponent } from '../HomePage/categories/categories.component';
import { CompaniesComponent } from '../HomePage/companies/companies.component';
import { StepsComponent } from '../HomePage/steps/steps.component';
import { HomearticlesComponent } from '../homearticles/homearticles.component';
import { AboutComponent } from '../HomePage/about/about.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HeaderComponent,AboutComponent,FooterComponent,ImgHeaderComponent,RouterLink,CategoriesComponent,CompaniesComponent,StepsComponent,HomearticlesComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}