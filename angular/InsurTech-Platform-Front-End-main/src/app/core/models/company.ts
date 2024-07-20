export class Company {
    constructor(
        public id: number,
        public name: string,
        public userName: string,
        public email: string,
        public taxNumber: string,
        public location: string,
        public insurancePlansCount: number,
        public status: string,
        public roles?: string[],
        public phoneNumber?: string,
    ) {}
}
