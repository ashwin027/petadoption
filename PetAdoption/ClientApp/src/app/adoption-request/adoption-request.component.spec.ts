import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdoptionRequestComponent } from './adoption-request.component';

describe('AdoptionRequestComponent', () => {
  let component: AdoptionRequestComponent;
  let fixture: ComponentFixture<AdoptionRequestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdoptionRequestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdoptionRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
