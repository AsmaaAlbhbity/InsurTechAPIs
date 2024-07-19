export enum RequestStatus {
    Pending = 0,
    Approved = 1,
    Rejected = 2
  }

 export  enum InsurancePlanLevel
  {
      basic=0,Standard=1, Premium=2
  }

export class CompanyRequests {

    constructor(
       public id:string,
       public customerName:string,
       public insurancePlanName:string,
       public  level:InsurancePlanLevel,
       public companyName:string,
       public  requestQuestions:any[],
       public status:RequestStatus
       
    ){}
}

