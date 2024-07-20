import { TestBed } from '@angular/core/testing';

import { MotorinsuranceService } from './motorinsurance.service';

describe('MotorinsuranceService', () => {
  let service: MotorinsuranceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MotorinsuranceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
