import { Injectable } from '@angular/core';
import { AppHttpServiceResponse } from '@app/classes/AppHttpServiceResponse.class';
import { IAppHttpResponse, ITrackHttpError } from '@app/interfaces/app-http-response.interface';
import { ILogDataChange } from '@app/interfaces/log-data-change.interface';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LogDataChangeService extends AppHttpServiceResponse {

  get(): Observable<IAppHttpResponse<ILogDataChange[]> | ITrackHttpError> {
    var url = `${environment.baseUrl}/logdatachange`;

    return this.http
      .get<IAppHttpResponse<ILogDataChange[]>>(url)
      .pipe(catchError((error) => this.handleHttpError(error)));
  }
}
