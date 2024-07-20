
export enum InsurancePlanLevel {
    Basic = 0,
    Standard = 1,
    Premium = 2
  }
export class AddHomeInsurance {
    constructor(public yearlyCoverage:number,public level:number,public quotation:number,public companyId:string,public waterDamage:number,public glassBreakage:number ,public naturalHazard:number,public attemptedTheft:number,public firesAndExplosion:number){

    }
}
