import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import {ArticaldataService} from '../../core/services/articles/article.service'
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-homearticles',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './homearticles.component.html',
  styleUrl: './homearticles.component.css'
})
export class HomearticlesComponent {
  constructor(private articaldataService: ArticaldataService) {}
  
  articles: any[] = [];
  limitedArticles: any[] = [];

  ngOnInit(): void {
    this.fetchArticles();
  }

  fetchArticles(): void {
    this.articaldataService.getArticleData().subscribe({
      next: (data) => {
        this.articles = data;
        this.limitedArticles = this.articles.slice(0, 3); // Get only the first 3 articles
      },
      error: (error) => {
        console.log(error);
      }
    });

  }

  truncateContent(content: string, limit: number = 40): string {
    return content.length > limit ? content.substring(0, limit) + '     ..........' : content;
  }

}
