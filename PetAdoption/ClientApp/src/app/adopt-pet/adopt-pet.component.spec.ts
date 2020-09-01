import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdoptPetComponent } from './adopt-pet.component';

describe('AdoptPetComponent', () => {
  let component: AdoptPetComponent;
  let fixture: ComponentFixture<AdoptPetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdoptPetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdoptPetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
