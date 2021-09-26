import { TestBed } from '@angular/core/testing';

import { SalesOrderSearchService } from './sales-order.service';

describe('SalesOrderSearchService', () => {
  let service: SalesOrderSearchService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SalesOrderSearchService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
