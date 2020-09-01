import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Telephone } from '../models/telephone';
import { AdopterDetailsDialogData } from '../models/adopterDetailsDialogData';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AdoptPetDialogData } from '../models/adoptPetDialogData';

@Component({
  selector: 'app-adopt-pet',
  templateUrl: './adopt-pet.component.html',
  styleUrls: ['./adopt-pet.component.scss']
})
export class AdoptPetComponent implements OnInit {
  adoptPetForm = this.fb.group({
    givenName: ['', Validators.required],
    lastName: ['', Validators.required],
    address: ['', Validators.required],
    telephone: [new Telephone('', '', ''), Validators.required]
  });
  constructor(private fb: FormBuilder, public dialogRef: MatDialogRef<AdoptPetComponent>, @Inject(MAT_DIALOG_DATA) public data: AdoptPetDialogData) { }

  ngOnInit(): void {
    this.adoptPetForm.patchValue({
      givenName: this.data.profile.given_name,
      lastName: this.data.profile.family_name,
    });
  }

  onSubmit() {
    let adopterDetailsDialogData: AdopterDetailsDialogData = {
      givenName: this.adoptPetForm.value.givenName,
      lastName: this.adoptPetForm.value.lastName,
      address: this.adoptPetForm.value.address,
      userId: this.data.profile.sub,
      telephone: this.adoptPetForm.value.telephone,
    }
    this.dialogRef.close(adopterDetailsDialogData);
  }

}
