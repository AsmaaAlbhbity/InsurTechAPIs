export interface InsurancePlan {
  id: number;
  company: string;
  category: string;
  level: number;
}

export interface ApiResponse {
  healthInsurancePlans: InsurancePlan[];
  motorInsurancePlans: InsurancePlan[];
  homeInsurancePlans: InsurancePlan[];
}
