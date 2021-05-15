export interface IAppHttpResponse<T> {
  data?: T;
  success: boolean;
  errors?: Error[];
}

interface Error {
  key: string;
  message: string;
}

export class ITrackHttpError implements IAppHttpResponse<any> {
  data: any;
  success: boolean;
  errors: Error[];
  friendlyMessage: string;
}
