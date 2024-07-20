import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BASE_URL } from '../base-url';
import { article } from '../models/article';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {

  constructor(private http: HttpClient) { }

  getArticles() {
    return this.http.get(`${BASE_URL}/articles`)
  }

  addArticle(article: article) {
    return this.http.post(`${BASE_URL}/articles`, article)
  }

  updateArticle(article: article) {
    return this.http.put(`${BASE_URL}/articles/${article.id}`, article)
  }

  deleteArticle(id: number) {
    return this.http.delete(`${BASE_URL}/articles/${id}`)
  }
}
