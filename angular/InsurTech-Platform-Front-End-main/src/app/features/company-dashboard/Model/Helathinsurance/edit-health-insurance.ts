
export enum InsurancePlanLevel {
    Basic = 0,
    Standard = 1,
    Premium = 2
  }
  
export class EditHealthInsurance {
    constructor(public id:number,
        public yearlyCoverage:number
        ,public level:number
        ,public quotation:number
        ,public companyId:string
        ,public medicalNetwork:string
        ,public clinicsCoverage:number
        ,public hospitalizationAndSurgery:number
        ,public opticalCoverage:number
        ,public dentalCoverage:number ){
  
    }
  
}
