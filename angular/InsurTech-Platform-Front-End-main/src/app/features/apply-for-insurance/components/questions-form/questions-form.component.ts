import { Component, Input, input } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { QuestionsFormService } from '../../../../core/services/questions-form.service';
import { Question } from '../../../../core/models/question';
import {MatInputModule} from '@angular/material/input';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS, MatFormFieldModule } from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import Swal from 'sweetalert2';
import { ButtonModule } from 'primeng/button';
import { TabViewModule } from 'primeng/tabview';



@Component({
  selector: 'app-questions-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatInputModule,
    FormsModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatButtonModule,
    ButtonModule,
    TabViewModule
  ],
  providers: [
    {provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {appearance: 'outline'}}
  ],
  templateUrl: './questions-form.component.html',
  styleUrl: './questions-form.component.css'
})
export class QuestionsFormComponent {
  
  // properties
  questions: Question[] = [];
  form: FormGroup;
  activeIndex: number = 0;
  @Input() customerId: string = "1";
  @Input() categoryId: number = 1;
  @Input() insurancePlanId: number = 2;

  // constructor
  constructor(
    private service: QuestionsFormService,
    private fb: FormBuilder
  ) 
  { 
    this.form = this.fb.group({});
  }


  // initialization
  ngOnInit() {
    this.service.GetById(this.categoryId).subscribe(data => {
      this.questions = data;
      this.questions.forEach(q => {
        this.form.addControl(q.id.toString(), this.fb.control('', [Validators.required]));
      });
    });
  }

  // methods
  Next(){
    if(this.activeIndex < this.questions.length - 1){
      this.activeIndex++;
    }
  }

  Previous(){
    if(this.activeIndex > 0){
      this.activeIndex--;
    }
  }

  Submit(){
    if(this.form.valid){
      let request: {
        customerId: string,
        insurancePlanId: number,
        answers: { questionId: number, answer: string }[]
      } = {
        customerId: "",
        insurancePlanId: 0,
        answers: []
      };
      request.customerId = this.customerId;
      request.insurancePlanId = this.insurancePlanId;
      for(let key in this.form.value){
        request.answers.push({
          questionId: parseInt(key),
          answer: this.form.value[key]
        });
      }
      this.service.CreateInsurancePlanRequest(request).subscribe(data => {
        Swal.fire({
          position: "top-end",
          icon: "success",
          title: "Request has been sent successfully!",
          showConfirmButton: false,
          timer: 1500
        });
      }, error => {
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          text: error.error.message || 'Something went wrong!',
        });
      }
      );
    }
    else{
      Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Please fill all the fields!',
      });
    }
  }
}
