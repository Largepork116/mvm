import { IPerson } from "./person.interface";

export interface IUser {
  email?: string;
  name?: string;
  role?: string;
  roleId?: number;
  userId?: number;

  password?: string;
  person?: IPerson;
}
