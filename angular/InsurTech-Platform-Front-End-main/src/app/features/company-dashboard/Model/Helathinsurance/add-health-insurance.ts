// src/app/models/Healthinsurance/add-health-insurance.ts

export enum InsurancePlanLevel {
    Basic = 0,
    Standard = 1,
    Premium = 2
  }
  
  export class AddHealthInsurance {
    constructor(
      public yearlyCoverage: number,
      public level: InsurancePlanLevel,
      public quotation: number,
      public companyId: string,
      public medicalNetwork: string,
      public clinicsCoverage: number,
      public hospitalizationAndSurgery: number,
      public opticalCoverage: number,
      public dentalCoverage: number,
      public categoryId: number = 1 // Default value
    ) {}
  }
  