import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA, LOCALE_ID } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import localeEs from '@angular/common/locales/es';
import { registerLocaleData } from '@angular/common';
registerLocaleData(localeEs, 'es');

import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { NgxSpinnerModule } from 'ngx-spinner';

import { LoaderInterceptor } from '@core/interceptor/loader.interceptor';
import { ErrorInterceptor } from '@core/interceptor/error.interceptor';
import { AuthInterceptor } from '@core/interceptor/auth.interceptor';
import { NoAuthGuard } from '@core/guards/no-auth.guard';
import { AuthGuard } from '@core/guards/auth.guard';

@NgModule({
  declarations: [],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [
    HttpClientModule,
    NgxSpinnerModule,
    BrowserAnimationsModule,
    SweetAlert2Module.forRoot()
  ],
  exports: [
    HttpClientModule,
    NgxSpinnerModule
  ],
  providers: [
    AuthGuard,
    NoAuthGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    },
    {
        provide: HTTP_INTERCEPTORS,
        useClass: AuthInterceptor,
        multi: true,
    },
    {
        provide: HTTP_INTERCEPTORS,
        useClass: LoaderInterceptor,
        multi: true,
    },
    {
      provide: LOCALE_ID,
      useValue: 'es'
    }
    
  ]
})
export class CoreModule { }
