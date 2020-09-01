import { TestBed } from '@angular/core/testing';

import { AdopterDetailService } from './adopter-detail.service';

describe('AdopterDetailService', () => {
  let service: AdopterDetailService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AdopterDetailService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
