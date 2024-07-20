import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AccordionModule } from 'primeng/accordion';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { HeaderComponent } from '../../shared/components/header/header.component';
import { BASE_URL } from '../../core/base-url';

interface FAQ {
  id: number;
  question: string;
  answer: string;
}

@Component({
  selector: 'app-faq',
  standalone: true,
  imports: [AccordionModule ,CommonModule,ButtonModule, HeaderComponent],
  templateUrl: './faq.component.html',
  styleUrl: './faq.component.css'
})
export class FAQComponent implements OnInit{
  faqs: any[] = [];
  activeIndex: number | number[] = -1;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.fetchFAQs();
  }

  fetchFAQs() {
    this.http.get<any[]>(`${BASE_URL}/FAQs`)
      .subscribe(
        data => {
          this.faqs = data;
        },
        error => {
          console.error('Error fetching FAQs', error);
          
        }
      );
  }

   activeIndexChange(event: any) {
    this.activeIndex = event;
  }

  setActiveIndex(index: number | number[]) {
    this.activeIndex = index;
  }

}









