/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { LogDataChangeService } from './log-data-change.service';

describe('Service: DataLogChange', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LogDataChangeService]
    });
  });

  it('should ...', inject([LogDataChangeService], (service: LogDataChangeService) => {
    expect(service).toBeTruthy();
  }));
});
