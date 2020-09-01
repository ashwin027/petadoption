import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AdopterRequestDialogData } from '../models/adopterRequestDialogData';

@Component({
  selector: 'app-adoption-request',
  templateUrl: './adoption-request.component.html',
  styleUrls: ['./adoption-request.component.scss']
})
export class AdoptionRequestComponent implements OnInit {

  constructor(private fb: FormBuilder, public dialogRef: MatDialogRef<AdoptionRequestComponent>, @Inject(MAT_DIALOG_DATA) public data: AdopterRequestDialogData) { }

  ngOnInit(): void {
  }
}
