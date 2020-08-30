import { TestBed } from '@angular/core/testing';

import { PetInfoService } from './pet-info.service';

describe('PetInfoService', () => {
  let service: PetInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PetInfoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
