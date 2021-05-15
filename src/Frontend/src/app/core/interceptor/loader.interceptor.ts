import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { finalize, tap } from 'rxjs/operators';

import { LoaderService } from '@services/others/loader.service';

@Injectable()
export class LoaderInterceptor implements HttpInterceptor{

  count = 0;
  constructor(private spinner: LoaderService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    this.spinner.show();

    this.count++;

    return next.handle(req)
        .pipe ( tap (
            ), finalize(() => {
                this.count--;
                if ( this.count == 0 ) this.spinner.hide();
            })
        );
    }

}