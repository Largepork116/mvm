import { HttpClient, HttpErrorResponse } from '@angular/common/http' 
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';

import { environment } from '@environments/environment';
import { ITrackHttpError } from '@interfaces/app-http-response.interface';


@Injectable()
export class AppHttpServiceResponse {
    public url = environment.baseUrl;
    public isProduction = environment.production;

    constructor(protected http: HttpClient) {
      
    }

    handleHttpError(error: HttpErrorResponse): Observable<ITrackHttpError> {
        console.log("ERROR => ", error);
        let dataError = new ITrackHttpError();
        dataError.errors = error.error;
        dataError.success = false;
        dataError.friendlyMessage = "Un error a ocurrido obteniendo los datos.";
        return throwError(dataError);
    }
}
