import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Question } from '../../../core/models/question';
import { SelectButtonModule } from 'primeng/selectbutton';
import { ListboxModule } from 'primeng/listbox';
import { QuestionsFormService } from '../../../core/services/questions-form.service';

@Component({
  selector: 'app-question-card',
  standalone: true,
  imports: [CommonModule, FormsModule, SelectButtonModule, ListboxModule],
  templateUrl: './question-card.component.html',
  styleUrls: ['./question-card.component.css'],
})
export class QuestionCardComponent implements OnChanges {
  @Input() question: Question | null = null;
  @Input() totalQuestions: number = 0;
  @Input() currentIndex: number = 0;
  answer: string = '';
  selectedValue: any; // For SelectButton
  selectedCity: string = ''; // For Listbox
  isPositiveNumberValid: boolean = true; // For positive number validation

  stateOptions: { label: string; value: any }[] = [
    { label: 'house', value: 'House' },
    { label: 'tenant', value: 'Tenant' },
    // Add more options as needed
  ];

  cities: { name: string; code: string }[] = [
    { name: 'Alexandria', code: 'AL' },
    { name: 'Aswan', code: 'Aswan' },
    { name: 'Assiut', code: 'AST' },
    { name: 'Al-Sharqia', code: 'SH' },
    { name: 'Beheira', code: 'BH' },
    { name: 'Beni Suef', code: 'BS' },
    { name: 'Cairo', code: 'CAI' },
    { name: 'Dakhlia', code: 'Dk' },
    { name: 'Demitta', code: 'dm' },
    { name: 'Fayoum', code: 'FY' },
    { name: 'Gharbia', code: 'GH' },
    { name: 'Giza', code: 'GZ' },
    { name: 'Ismailia', code: 'IS' },
    { name: 'KafrElshikh', code: 'Kafr' },
    { name: 'Matrouh', code: 'MTR' },
    { name: 'Minya', code: 'MIN' },
    { name: 'Monofia', code: 'MON' },
    { name: 'New Valley', code: 'NV' },
    { name: 'North Sinai', code: 'NS' },
    { name: 'Port Said', code: 'PS' },
    { name: 'Qualyubia', code: 'QB' },
    { name: 'Qena', code: 'QN' },
    { name: 'Red Sea', code: 'RS' },
    { name: 'Sohag', code: 'SG' },
    { name: 'South Sinai', code: 'SS' },
    { name: 'Suez', code: 'SZ' },
    { name: 'Luxor', code: 'luxor' },
    // Add more cities as needed
  ];

  constructor(
    public questionService: QuestionsFormService,
    private router: Router
  ) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['currentIndex']) {
      console.log('Current Index Changed:', this.currentIndex);
    }
    if (changes['question']) {
      this.question = changes['question'].currentValue;
    }
  }

  isNextButtonDisabled(): boolean {
    if (this.currentIndex === 0 && this.selectedValue === undefined) {
      return true;
    }
    if (this.currentIndex === 1 && this.selectedCity === '') {
      return true;
    }
    if (this.currentIndex === 2 && this.answer === '') {
      return true;
    }
    if (
      this.currentIndex === this.totalQuestions - 1 &&
      (this.answer === '' || !this.isPositiveNumberValid)
    ) {
      return true;
    }
    return false;
  }

  NextQuestion() {
    if (
      this.questionService.questIndex ===
      this.questionService.questionArr.length - 1
    ) {
      this.router.navigate(['insurancePlans']);
      console.log('answers', this.questionService.GetAnswers());
    } else {
      this.questionService.NextQuestion();
      this.answer = '';
      this.selectedValue = undefined;
      this.selectedCity = '';
    }
  }

  PreviousQuestion() {
    this.questionService.PreviousQuestion();
    this.answer = '';
    this.selectedValue = undefined;
    this.selectedCity = '';
  }

  AddAnswer() {
    if (this.currentIndex === 0 && this.selectedValue !== undefined) {
      this.questionService.AddAnswer({
        questionId: this.question?.id || 0,
        answer: this.selectedValue,
      });
    }
    if (this.currentIndex === 1 && this.selectedCity) {
      this.questionService.AddAnswer({
        questionId: this.question?.id || 0,
        answer: this.selectedCity,
      });
    }
    if (this.currentIndex === 2 && this.answer !== '') {
      this.questionService.AddAnswer({
        questionId: this.question?.id || 0,
        answer: this.answer,
      });
    }
    if (this.currentIndex === this.totalQuestions - 1 && this.answer !== '') {
      this.questionService.AddAnswer({
        questionId: this.question?.id || 0,
        answer: this.answer,
      });
    }
    this.NextQuestion();
  }

  validatePositiveNumber() {
    this.isPositiveNumberValid = parseFloat(this.answer) > 0;
  }
}
