 export class Adminquestions {
   constructor(
     public id: number,
     public body: string,
     public categoryId: number,
     public questiontype: string,
     public options: string,
     public Placeholder: string,
     public optionsArray?: string[]
   ) {}
 }