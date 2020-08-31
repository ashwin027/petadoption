import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdoptionDetailsDialogComponent } from './adoption-details-dialog.component';

describe('AdoptionDetailsDialogComponent', () => {
  let component: AdoptionDetailsDialogComponent;
  let fixture: ComponentFixture<AdoptionDetailsDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdoptionDetailsDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdoptionDetailsDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
