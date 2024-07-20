import { InsurancePlanLevel } from './Insurance-plan-level.enum';
export class HomeInsurancePlan {
    constructor(
      public id: number,
      public yearlyCoverage: number,
      public level: InsurancePlanLevel,
      public category: string,
      public quotation: number,
      public company: string,
      public waterDamage: number,
      public glassBreakage: number,
      public naturalHazard: number,
      public attemptedTheft: number,
      public firesAndExplosion: number,
      public numberOfUsers: number
    ) {
        this.id = id;
        this.yearlyCoverage = yearlyCoverage;
        this.level = level;
        this.category = category;
        this.quotation = quotation;
        this.company = company;
        this.waterDamage = waterDamage;
        this.glassBreakage = glassBreakage;
        this.naturalHazard = naturalHazard;
        this.attemptedTheft = attemptedTheft;
        this.firesAndExplosion = firesAndExplosion;
        this.numberOfUsers = numberOfUsers;
      }
  }
  
