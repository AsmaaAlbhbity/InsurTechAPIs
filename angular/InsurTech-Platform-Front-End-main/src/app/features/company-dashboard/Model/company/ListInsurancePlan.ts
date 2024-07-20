import { HealthInsurancePlan } from "./HealthInsurancePlan";
import { HomeInsurancePlan } from "./HomeInsurancePlan";
import { MotorInsurancePlan } from "./MotorInsurancePlan";

export class ListInsurancePlan {


    constructor(public healthInsurancePlans: HealthInsurancePlan[] | null,
        public motorInsurancePlans: MotorInsurancePlan[] | null,
        public homeInsurancePlans: HomeInsurancePlan[] | null
    ) { }
}