import { InsurancePlanLevel } from './Insurance-plan-level.enum';

export class MotorInsurancePlan {
  constructor(
    public id: number,
    public yearlyCoverage: number,
    public level: InsurancePlanLevel,
    public category: string,
    public quotation: number,
    public company: string,
    public personalAccident: number,
    public theft: number,
    public thirdPartyLiability: number,
    public ownDamage: number,
    public legalExpenses: number,
    public numberOfUsers: number
  ) {
    this.id = id;
    this.yearlyCoverage = yearlyCoverage;
    this.level = level;
    this.category = category;
    this.quotation = quotation;
    this.company = company;
    this.personalAccident = personalAccident;
    this.theft = theft;
    this.thirdPartyLiability = thirdPartyLiability;
    this.ownDamage = ownDamage;
    this.legalExpenses = legalExpenses;
    this.numberOfUsers = numberOfUsers;
  }
}
