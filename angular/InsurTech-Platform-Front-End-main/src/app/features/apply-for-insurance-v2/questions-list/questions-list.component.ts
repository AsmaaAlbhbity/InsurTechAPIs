import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { QuestionsFormService } from '../../../core/services/questions-form.service';
import { QuestionCardComponent } from '../question-card/question-card.component';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import { HeaderComponent } from '../../../shared/components/header/header.component';

@Component({
  selector: 'app-questions-list',
  standalone: true,
  imports: [QuestionCardComponent, CommonModule, HeaderComponent],
  templateUrl: './questions-list.component.html',
  styleUrl: './questions-list.component.css',
})
export class QuestionsListComponent implements OnInit, OnDestroy {
  routeSub: Subscription | null = null;
  dataSub: Subscription | null = null;
  catId: number = 0;
  questionArr: any = [];

  constructor(
    private route: ActivatedRoute,
    public QuestionService: QuestionsFormService
  ) {}

  ngOnInit(): void {
    this.routeSub = this.route.params.subscribe((params) => {
      this.catId = params['id'];
      sessionStorage.setItem('catId', this.catId.toString());

      this.dataSub = this.QuestionService.GetById(this.catId).subscribe(
        (data) => {
          this.questionArr = data;
          this.QuestionService.questionArr = data;
        }
      );
    });
  }
  ngOnDestroy(): void {
    this.routeSub?.unsubscribe();
    this.dataSub?.unsubscribe();
  }
}
