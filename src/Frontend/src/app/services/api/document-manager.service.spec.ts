/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { DocumentManagerService } from './aircraft.service';

describe('Service: Aircraft', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AircraftService]
    });
  });

  it('should ...', inject([DocumentManagerService], (service: DocumentManagerService) => {
    expect(service).toBeTruthy();
  }));
});
