import { ICompany } from './company.interface';

export interface IPerson {
    personId?: number;
    name?: string;
    lastName?: string;
    phone?: string;
    companyId?: number;
    company?: ICompany;
    user?: any;
}
