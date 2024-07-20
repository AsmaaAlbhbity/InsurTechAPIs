export interface InsuranceReview {
  id: number;
  rating: number;
  comment: string;
  customerId: string;
  customerName: string;
  insurancePlanId: number;
  insurancePlanName: string;
  insurancePlanLevel: string;
  catId: number;
  yearlyCoverage: string;
}
