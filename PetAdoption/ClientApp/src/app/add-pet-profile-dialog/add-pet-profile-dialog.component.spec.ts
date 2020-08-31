import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPetProfileDialogComponent } from './add-pet-profile-dialog.component';

describe('AddPetProfileDialogComponent', () => {
  let component: AddPetProfileDialogComponent;
  let fixture: ComponentFixture<AddPetProfileDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddPetProfileDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddPetProfileDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
