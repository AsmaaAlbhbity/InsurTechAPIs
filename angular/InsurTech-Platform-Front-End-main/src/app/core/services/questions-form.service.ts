import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BASE_URL } from '../base-url';
import { Question } from '../models/question';

import { Answer } from '../types/Answer';
import { Observable } from 'rxjs';
import { Adminquestions } from '../models/AdminQuestions';

@Injectable({
  providedIn: 'root',
})
export class QuestionsFormService {
  questionArr: any = [];
  questIndex: number = 0;
  answers: { questionId: number; answer: string }[] = [];
  currentCategory: number = 0;

  constructor(private http: HttpClient) {}

  GetById(id: number) {
    return this.http.get<Question[]>(
      BASE_URL + '/Customers/GetQusetionsByCategory/' + id
    );
  }

  CreateInsurancePlanRequest(request: any) {
    return this.http.post(
      BASE_URL + '/Customers/requestInsurancePlan',
      request
    );
  }
  GetQuestiosArr(id: number) {
    this.GetById(id).subscribe((data) => {
      this.questionArr = data;
      console.log(this.questionArr, 'questionArr from the service ya m3rs');
    });
    console.log(this.questionArr, 'questionArr after the service ya m3rs');
  }
  getQuestion() {
    return this.questionArr[this.questIndex];
  }
  NextQuestion() {
    if (this.questIndex < this.questionArr.length - 1) {
      this.questIndex++;
    }
  }
  PreviousQuestion() {
    if (this.questIndex > 0) {
      this.questIndex--;
    }
  }
  AddAnswer(answer: Answer) {
    //we can find if the there is an answer for this question
    const index = this.answers.findIndex(
      (x) => x.questionId == answer.questionId
    );
    if (index != -1) {
      this.answers[index] = answer;
      return;
    }

    this.answers.push(answer);
  }
  GetAnswers() {
    return this.answers;
  }
  getallquestionsArray(): Observable<Adminquestions[]> {
    return this.http.get<Adminquestions[]>(`${BASE_URL}/questions`);
  }
  DeleteQuestions(id: number) {
    return this.http.delete(`http://localhost:5028/api/questions/ ${id}`);
  }
  addQuestion(question: Adminquestions): Observable<Adminquestions> {
    return this.http.post<Adminquestions>(`${BASE_URL}/questions`, question);
  }
}
