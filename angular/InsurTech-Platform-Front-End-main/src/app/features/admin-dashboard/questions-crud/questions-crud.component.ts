import { Component, OnInit } from '@angular/core';
import { QuestionsFormService } from '../../../core/services/questions-form.service';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { TableModule } from 'primeng/table';
import { Adminquestions } from '../../../core/models/AdminQuestions';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';
import { QuestionType } from '../../../core/models/Home_Page/question-type.enum';

@Component({
  selector: 'app-questions-crud',
  standalone: true,
  imports: [
    FormsModule,
    TableModule,
    CommonModule,
    ButtonModule,
    DialogModule,
    DropdownModule,
  ],
  templateUrl: './questions-crud.component.html',
  styleUrls: ['./questions-crud.component.css'],
})
export class QuestionsCrudComponent implements OnInit {
  questions: Adminquestions[] = [];
  questions_health: Adminquestions[] = [];
  questions_car: Adminquestions[] = [];
  questions_real: Adminquestions[] = [];
  options: any[] = [];
  newQuestion: Adminquestions = {
    id: 0,
    body: '',
    categoryId: 1, // Default category
    options: '',
    placeholder: '',
    questiontype: QuestionType.Text,
  };
  displayDialog: boolean = false;
  questionTypes: { label: string; value: QuestionType }[] = [];

  constructor(public question: QuestionsFormService) {}

  ngOnInit(): void {
    this.question.getallquestionsArray().subscribe((data) => {
      this.questions = data.map((question) => ({
        ...question,
        type: QuestionType[question.questiontype as unknown as keyof typeof QuestionType], // Convert number to enum
        optionsArray: question.options
          ? question.options.split(',').map((option) => option.trim())
          : [],
      }));
      console.log(this.questions);

      this.questions_health = this.questions.filter(
        (data) => data.categoryId == 1
      );
      this.questions_car = this.questions.filter(
        (data) => data.categoryId == 2
      );
      this.questions_real = this.questions.filter(
        (data) => data.categoryId == 3
      );
    });

    this.questionTypes = Object.keys(QuestionType).map((key) => ({
      label: key,
      value: QuestionType[key as keyof typeof QuestionType],
    }));
  }

  updateCategoryQuestions(): void {
    this.questions_health = this.questions.filter((q) => q.categoryId === 1);
    this.questions_car = this.questions.filter((q) => q.categoryId === 2);
    this.questions_real = this.questions.filter((q) => q.categoryId === 3);
  }

  getDropdownOptions(question: Adminquestions) {
    return question.optionsArray?.map((option) => ({
      label: option,
      value: option,
    }));
  }

  deleteQuestion(id: number) {
    this.question.DeleteQuestions(id).subscribe((data) => {
      this.ngOnInit();
    });
  }

  addQuestion(): void {
    this.question.addQuestion(this.newQuestion).subscribe((addedQuestion) => {
      console.log(addedQuestion);
      this.questions.push(addedQuestion);
      this.updateCategoryQuestions();
      this.resetNewQuestion();
      this.ngOnInit();
      this.displayDialog = false; // Close the dialog
    });
  }

  openDialog(): void {
    this.displayDialog = true;
  }

  resetNewQuestion(): void {
    this.newQuestion = {
      id: 0,
      body: '',
      categoryId: 1,
      questiontype: QuestionType.Text,
      options: '',
      placeholder: '',
    };
  }
}
