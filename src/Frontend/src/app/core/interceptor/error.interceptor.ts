import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';

import { AuthService } from '@services/api/auth.service';
import { NotificationService } from '@services/others/notification.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor{

  constructor(private router: Router, private authService: AuthService, private notificationService: NotificationService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(req).pipe(
      catchError((error: any) => {

        console.log("ERROR => ", error);
        
        let errorMessageType = ['Auth', 'FieldValidations'];
        let infoMessageType = ['Business', 'NotFound'];

        if([400, 404].indexOf(error.status) != -1) {

          if(error.error instanceof Blob){
            if(error.status == 404){
              this.notificationService.toast('No se encontraron datos para la generación del archivo', 'error');
            }
            return throwError("blob error");
          }

          for(var e of error.error.errors) {
            if(errorMessageType.indexOf(e.key) != -1) {
              this.notificationService.toast(e.message, 'error');
            }

            if(infoMessageType.indexOf(e.key) != -1) {
              this.notificationService.toast(e.message, 'warning');
            }

            if(e.key == 'Unknown') {
              this.notificationService.toast('Se produjo un error interno. Intente nuevamente.', 'error');
            }
          }
        }

        if(error.status == 500) {
          this.notificationService.toast('Se produjo un error interno. Intente nuevamente.', 'error');
        }

        if([401,403].indexOf(error.status) != -1) {
          this.authService.logout();
          this.router.navigateByUrl("login");
        }
        return throwError(error);

      })
    );
  }

}
