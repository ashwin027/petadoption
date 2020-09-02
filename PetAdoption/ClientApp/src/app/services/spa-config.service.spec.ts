import { TestBed } from '@angular/core/testing';

import { SpaConfigService } from './spa-config.service';

describe('SpaConfigService', () => {
  let service: SpaConfigService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SpaConfigService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
