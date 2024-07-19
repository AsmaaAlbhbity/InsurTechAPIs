
export enum InsurancePlanLevel {
    Basic = 0,
    Standard = 1,
    Premium = 2
  }
export class AddMotorInsurance {
    
  constructor(public yearlyCoverage:number,public level:number,public quotation:number,public companyId:string,public personalAccident:number,public theft:number,public thirdPartyLiability:number,public ownDamage:number ,public legalExpenses:number){

  }
}
