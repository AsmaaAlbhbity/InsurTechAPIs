import { TestBed } from '@angular/core/testing';

import { CompanyRequestsService } from './company-requests.service';

describe('CompanyRequestsService', () => {
  let service: CompanyRequestsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CompanyRequestsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
