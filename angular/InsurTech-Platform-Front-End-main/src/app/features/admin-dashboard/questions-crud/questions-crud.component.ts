import { Component, OnInit } from '@angular/core';
import { QuestionsFormService } from '../../../core/services/questions-form.service';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { TableModule } from 'primeng/table';
import { Adminquestions } from '../../../core/models/AdminQuestions';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';

@Component({
  selector: 'app-questions-crud',
  standalone: true,
  imports: [FormsModule, TableModule, CommonModule, ButtonModule, DialogModule],
  templateUrl: './questions-crud.component.html',
  styleUrl: './questions-crud.component.css',
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
    Placeholder: '',
    questiontype: '',
  };
  displayDialog: boolean = false;

  constructor(public question: QuestionsFormService) {}
  ngOnInit(): void {
    this.question.getallquestionsArray().subscribe((data) => {
      this.questions = data;
      console.log(this.questions);
      // // this.questions.forEach((q) => {
      // //   q.optionsArray = q.Options.split(',').map((option) => option.trim());
      // //   console.log(q.optionsArray);

      // });
      for (let index = 0; index < this.questions.length; index++) {
        console.log(this.questions[index].options);

        // Check if Options is defined and is a string
        console.log(this.questions[index].options);
        if (typeof this.questions[index].options === 'string') {
          this.questions[index].optionsArray = this.questions[index].options
            .split(',')
            .map((option) => option.trim());

          console.log(this.questions[index].optionsArray);
        } else {
          console.error(
            'Options is not defined or not a string:',
            this.questions[index].options
          );
        }
      }

      this.questions_health = this.questions.filter((data) => {
        return data.categoryId == 1;
      });
      this.questions_car = this.questions.filter((data) => {
        return data.categoryId == 2;
      });

      this.questions_real = this.questions.filter((data) => {
        return data.categoryId == 3;
      });
    });
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
      questiontype: '',
      options: '',
      Placeholder: '',
    };
  }
}
