export class HomePlan {
    constructor(
        public id: number,
        public yearlyCoverage: number,
        public level: number,
        public category: string,
        public quotation: number,
        public company: string,
        public waterDamage: number,
        public glassBreakage: number,
        public naturalHazard: number,
        public attemptedTheft: number,
        public firesAndExplosion: number,
        public numberOfUsers: number
    ) {}
}
