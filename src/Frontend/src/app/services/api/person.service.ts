import { Injectable } from '@angular/core';
import { AppHttpServiceResponse } from '@app/classes/AppHttpServiceResponse.class';
import { IAppHttpResponse, ITrackHttpError } from '@app/interfaces/app-http-response.interface';
import { IPerson } from '@app/interfaces/person.interface';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PersonService extends AppHttpServiceResponse {

  get(): Observable<IAppHttpResponse<IPerson[]> | ITrackHttpError> {
    var url = `${environment.baseUrl}/person`;

    return this.http
      .get<IAppHttpResponse<IPerson[]>>(url)
      .pipe(catchError((error) => this.handleHttpError(error)));
  }

}
