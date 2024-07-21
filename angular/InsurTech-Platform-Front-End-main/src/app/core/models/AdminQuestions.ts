import { QuestionType } from "./Home_Page/question-type.enum";

 
 export class Adminquestions {
   constructor(
     public id: number,
     public body: string,
     public categoryId: number,
     public questiontype: QuestionType,
     public options: string,
     public placeholder: string,
     public optionsArray?: string[]
   ) {}
 }