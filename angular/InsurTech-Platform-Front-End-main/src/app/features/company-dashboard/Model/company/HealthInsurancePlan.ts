import { InsurancePlanLevel } from './Insurance-plan-level.enum';
export class HealthInsurancePlan
{
    
     constructor( public id:number,
        public yearlyCoverag:number,
        public level:InsurancePlanLevel,
        public category:string,
        public quotation:number,
        public company:string,
        public medicalNetwork:string,
        public clinicsCoverage:number,
        public hospitalizationAndSurgery:number,
        public opticalCoverage:number,
        public dentalCoverage:number,
        public numberOfUsers:number) {

        this.id=id;
        this.yearlyCoverag=yearlyCoverag;
        this.level=level;
        this.category=category;
        this.quotation=quotation;
        this.company=company;
        this.medicalNetwork=medicalNetwork;
        this.clinicsCoverage=clinicsCoverage;
        this.hospitalizationAndSurgery=hospitalizationAndSurgery;
        this.opticalCoverage=opticalCoverage;
        this.dentalCoverage=dentalCoverage;
        this.numberOfUsers=numberOfUsers;
        }
}
