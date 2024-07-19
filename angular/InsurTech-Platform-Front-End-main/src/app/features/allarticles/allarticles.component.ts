import { Component, OnInit } from '@angular/core';
import {ArticaldataService} from '../../core/services/articles/article.service'
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from '../../shared/components/header/header.component';

@Component({
  selector: 'app-allarticles',
  standalone: true,
  imports: [FormsModule,CommonModule, HeaderComponent],
  templateUrl: './allarticles.component.html',
  styleUrl: './allarticles.component.css'
})
export class AllarticlesComponent  implements OnInit{
  constructor(private articaldataService: ArticaldataService) {}
  ngOnInit():void{
    this.fetchArticles();
  }
  articles: any[] = [];
  fetchArticles(): void {
    this.articaldataService.getArticleData().subscribe({
      next:(data)=>{
        this.articles = data;     
      },
      error:(error)=>{
        console.log(error);
      }
    }

      
    );
  }
}
