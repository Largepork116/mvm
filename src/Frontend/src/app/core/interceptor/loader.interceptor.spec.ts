/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { LoaderInterceptor } from './loader.interceptor';

describe('Service: Loader.interceptor', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LoaderInterceptor]
    });
  });

  it('should ...', inject([LoaderInterceptor], (service: LoaderInterceptor) => {
    expect(service).toBeTruthy();
  }));
});
