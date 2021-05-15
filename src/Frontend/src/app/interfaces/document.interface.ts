import { IPerson } from './person.interface';
import { IInternalFile } from './internal-file.interface';
import { IExternalFile } from './external-file.interface';

export interface IDocument {
  documentId?: number;
  type?: string;
  internalFileId?: number;
  internalFile?: IInternalFile;
  externalFileId?: number;
  externalFile?: IExternalFile;
  senderId?: number;
  sender?: IPerson;
  addresseeId?: number;
  addressee?: IPerson;
  createdBy?: string;
  createdAt?: Date;
  updatedBy?: string;
  updatedAt?: Date;
}


  

