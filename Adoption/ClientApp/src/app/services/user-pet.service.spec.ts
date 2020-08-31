import { TestBed } from '@angular/core/testing';

import { UserPetService } from './user-pet.service';

describe('UserPetService', () => {
  let service: UserPetService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserPetService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
