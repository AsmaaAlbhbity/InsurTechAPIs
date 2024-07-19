export class HealthPlan {
    constructor(
        public id: number,
        public yearlyCoverage: number,
        public level: number,
        public category: string,
        public quotation: number,
        public company: string,
        public medicalNetwork: string,
        public clinicsCoverage: number,
        public hospitalizationAndSurgery: number,
        public opticalCoverage: number,
        public dentalCoverage: number,
        public numberOfUsers: number
    ) {}
}
