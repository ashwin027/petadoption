import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { AdoptionAdditionalDetails } from '../models/adoptionAdditionalDetails';

@Component({
  selector: 'app-adoption-details-dialog',
  templateUrl: './adoption-details-dialog.component.html',
  styleUrls: ['./adoption-details-dialog.component.scss']
})
export class AdoptionDetailsDialogComponent implements OnInit {
  adoptionForm = this.fb.group({
    fees: [''],
    additionalRequirements: ['']
  });
  constructor(private fb: FormBuilder, public dialogRef: MatDialogRef<AdoptionDetailsDialogComponent>) { }

  ngOnInit(): void {
  }

  onSubmit() {
    let additionalDetails: AdoptionAdditionalDetails = {
      additionalRequirements: this.adoptionForm.value.additionalRequirements,
      fees: this.adoptionForm.value.fees
    }
    this.dialogRef.close(additionalDetails);
  }
}
