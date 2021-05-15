import { Injectable } from '@angular/core';
import { AppHttpServiceResponse } from '@app/classes/AppHttpServiceResponse.class';
import { IAppHttpResponse, ITrackHttpError } from '@app/interfaces/app-http-response.interface';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IDocument } from '../../interfaces/document.interface';
import { saveAs } from 'file-saver';

@Injectable({
  providedIn: 'root'
})

export class DocumentManagerService extends AppHttpServiceResponse {

    get(): Observable<IAppHttpResponse<IDocument[]> | ITrackHttpError> {
      var url = `${environment.baseUrl}/document`;
  
      return this.http
        .get<IAppHttpResponse<IDocument[]>>(url)
        .pipe(catchError((error) => this.handleHttpError(error)));
    }

    create(file: File, document: IDocument): Observable<IAppHttpResponse<any> | ITrackHttpError> {
      var url = `${environment.baseUrl}/document/file`;
  
      const formData: FormData = new FormData();
      formData.append('file', file, file.name);
      formData.append('senderId', '' + document.senderId);
      formData.append('addresseeId', '' + document.addresseeId);
      formData.append('type', '' + document.type);

      return this.http
        .post<IAppHttpResponse<any>>(url, formData)
        .pipe(catchError((error) => this.handleHttpError(error)));
    }

    getPdf(fileName: string) {
      var url = `${environment.baseUrl}/document/file/${fileName}/pdf`;
  
      this.http
        .post(url, null, { responseType: "blob" }) //set response Type properly (it is not part of headers)
        .toPromise()
        .then(blob => {
          saveAs(blob, `${fileName}.pdf`);
        })
        .catch(err => console.error("download error = ", err))
    }

    getMyDocuments(): Observable<IAppHttpResponse<IDocument[]> | ITrackHttpError> {
      var url = `${environment.baseUrl}/document/user/myself`;
  
      return this.http
        .get<IAppHttpResponse<IDocument[]>>(url)
        .pipe(catchError((error) => this.handleHttpError(error)));
    }

}
