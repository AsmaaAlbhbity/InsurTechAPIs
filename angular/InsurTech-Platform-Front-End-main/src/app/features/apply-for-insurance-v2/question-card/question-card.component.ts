// question-card.component.ts
import { Component, Input, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Question } from '../../../core/models/question';
import { SelectButtonModule } from 'primeng/selectbutton';
import { ListboxModule } from 'primeng/listbox';
import { QuestionsFormService } from '../../../core/services/questions-form.service';
import { Answer } from '../../../core/types/Answer';
import { QuestionType } from '../../../core/models/Home_Page/question-type.enum';

@Component({
  selector: 'app-question-card',
  standalone: true,
  imports: [CommonModule, FormsModule, SelectButtonModule, ListboxModule],
  templateUrl: './question-card.component.html',
  styleUrls: ['./question-card.component.css'],
})
export class QuestionCardComponent implements OnInit {
  @Input() question: Question | undefined;
  @Input() currentIndex: number = 0;
  @Input() totalQuestions: number = 0;
  stateOptions = [
    { label: 'Option 1', value: 1 },
    { label: 'Option 2', value: 2 },
  ]; // Example state options
  cities = [{ name: 'City 1' }, { name: 'City 2' }]; // Example cities
  selectedValue: any;
  selectedCity: any;
  answer: string = '';

  questionTypes = QuestionType; // Import and use the enum

  constructor(private questionsFormService: QuestionsFormService) {}

  ngOnInit(): void {
    this.questionsFormService.GetQuestionsArr(1); // Use category ID or any other identifier
    this.updateQuestion();
  }

  updateQuestion(): void {
    this.question = this.questionsFormService.getQuestion();
    console.log('Current question:', this.question);
  }

  NextQuestion(): void {
    this.questionsFormService.NextQuestion();
    this.updateQuestion();
  }

  PreviousQuestion(): void {
    this.questionsFormService.PreviousQuestion();
    this.updateQuestion();
  }

  AddAnswer(): void {
    const answer: Answer = {
      questionId: this.question?.id || 0,
      answer: this.answer,
    };
    this.questionsFormService.AddAnswer(answer);
    if (
      this.currentIndex ===
      this.questionsFormService['questions'].length - 1
    ) {
      // Handle form submission
    } else {
      this.NextQuestion();
    }
  }

  isNextButtonDisabled(): boolean {
    return !this.answer.trim();
  }
}
