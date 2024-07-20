// src/app/models/user.model.ts
export interface BaseUser {
  id: string;
  name: string;
  userName: string;
  email: string;
  phoneNumber: string;
  userType: number;
}

export interface Customer extends BaseUser {
  nationalId: string;
  birthDate: string;
}

export interface Company extends BaseUser {
  taxNumber: string;
  location: string;
}

export type User = Customer | Company;
