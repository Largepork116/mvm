import { Injectable } from '@angular/core';
import { AppHttpServiceResponse } from '@app/classes/AppHttpServiceResponse.class';
import { IAppHttpResponse, ITrackHttpError } from '@app/interfaces/app-http-response.interface';
import { IUser } from '@app/interfaces/user.interface';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService extends AppHttpServiceResponse {

  get(): Observable<IAppHttpResponse<IUser[]> | ITrackHttpError> {
    var url = `${environment.baseUrl}/user`;

    return this.http
      .get<IAppHttpResponse<IUser[]>>(url)
      .pipe(catchError((error) => this.handleHttpError(error)));
  }

  getRoles(): Observable<IAppHttpResponse<IUser[]> | ITrackHttpError> {
    var url = `${environment.baseUrl}/user/roles`;

    return this.http
      .get<IAppHttpResponse<IUser[]>>(url)
      .pipe(catchError((error) => this.handleHttpError(error)));
  }

  create(user: IUser): Observable<IAppHttpResponse<any> | ITrackHttpError> {
    var url = `${environment.baseUrl}/user`;

    return this.http
      .post<IAppHttpResponse<any>>(url, user)
      .pipe(catchError((error) => this.handleHttpError(error)));
  }

  update(user: IUser): Observable<IAppHttpResponse<any> | ITrackHttpError> {
    var url = `${environment.baseUrl}/user`;

    return this.http
      .put<IAppHttpResponse<any>>(url, user)
      .pipe(catchError((error) => this.handleHttpError(error)));
  }

}
