import {Component, Inject, OnInit} from '@angular/core';
import {MatDialog, MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import { FormBuilder, Validators, FormControl } from '@angular/forms';
import { Gender } from '../models/gender';
import { DogBreedInfo } from '../models/dogBreedInfo';
import { PetInfoService } from '../services/pet-info.service';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import {UserPet} from '../models/userPet';
import { AuthService } from '../services/auth.service';
import { UserProfile } from '../models/userProfile';
import {UserPetService} from '../services/user-pet.service';

@Component({
  selector: 'app-add-pet-profile-dialog',
  templateUrl: './add-pet-profile-dialog.component.html',
  styleUrls: ['./add-pet-profile-dialog.component.scss']
})
export class AddPetProfileDialogComponent implements OnInit {
  breedControl = new FormControl();
  genders = new Array<Gender>();
  breeds = new Array<DogBreedInfo>();
  filteredOptions: Observable<DogBreedInfo[]>;
  petProfileForm = this.fb.group({
    name: ['', Validators.required],
    description: ['', Validators.required],
    gender: ['', Validators.required],
    breed: ['', Validators.required]
  });
  profile: UserProfile;
  constructor(private fb: FormBuilder, private petInfoService: PetInfoService, private userPetService: UserPetService, 
    private auth: AuthService, public dialogRef: MatDialogRef<AddPetProfileDialogComponent>) {}

  ngOnInit(): void {
    this.genders.push({ displayText: 'Male', value: 'male' });
    this.genders.push({ displayText: 'Female', value: 'female' });
    this.genders.push({ displayText: 'Don\'t know', value: 'dontknow' });

    this.petInfoService.getAllBreeds().subscribe((breeds) => {
      this.breeds = breeds;
      this.filteredOptions = this.petProfileForm.get('breed').valueChanges.pipe(
        startWith(''),
        map(value => typeof value === 'string' ? value : value.name),
        map(name => name ? this._filter(name) : this.breeds.slice())
      );
    });

    this.auth.userProfile$.subscribe((prfl) =>{
      this.profile = prfl;
     });
  }

  displayFn(breedId: Number): string {
    if (this.breeds.length>0){
      let breed = this.breeds.filter(b => b.id===breedId)[0];
      if (breed){
        return breed?.name;
      }
      return '';
    }
    else{
      return '';
    }
  }

  private _filter(name: string): DogBreedInfo[] {
    const filterValue = name.toLowerCase();

    return this.breeds.filter(option => option.name.toLowerCase().indexOf(filterValue) === 0);
  }

  onSubmit() {
    // TODO: Use EventEmitter with form value
    let userPet: UserPet = {
      breedId: this.petProfileForm.value.breed,
      description: this.petProfileForm.value.description,
      gender: this.petProfileForm.value.gender,
      name: this.petProfileForm.value.name,
      userId: this.profile.sub
    }

    this.userPetService.createUserPet(userPet).subscribe((userPet) =>{
      this.dialogRef.close(userPet);
    });
  }

}
