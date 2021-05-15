/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ErrorInterceptor } from './error.interceptor';

describe('Service: Error.interceptor', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ErrorInterceptor]
    });
  });

  it('should ...', inject([ErrorInterceptor], (service: ErrorInterceptor) => {
    expect(service).toBeTruthy();
  }));
});
