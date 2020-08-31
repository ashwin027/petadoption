import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormControl, FormGroup } from '@angular/forms';
import { Gender } from '../models/gender';
import { DogBreedInfo } from '../models/dogBreedInfo';
import { PetInfoService } from '../services/pet-info.service';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { UserPet } from '../models/userPet';
import { AuthService } from '../services/auth.service';
import { UserProfile } from '../models/userProfile';
import { UserPetService } from '../services/user-pet.service';
import { AdoptionService } from '../services/adoption.service';
import { MatDialog } from '@angular/material/dialog';
import { AddPetProfileDialogComponent } from '../add-pet-profile-dialog/add-pet-profile-dialog.component';
import { MatSelectChange } from '@angular/material/select';
import { AdoptionDetailsDialogComponent } from '../adoption-details-dialog/adoption-details-dialog.component';
import { AdoptionAdditionalDetails } from '../models/adoptionAdditionalDetails';
import { Adoption } from '../models/adoption';

@Component({
  selector: 'app-pet-profile',
  templateUrl: './pet-profile.component.html',
  styleUrls: ['./pet-profile.component.scss']
})
export class PetProfileComponent implements OnInit {
  profile: UserProfile;
  userPets = new Array<UserPet>();
  selectedUserPet: UserPet;
  selectedBreed: DogBreedInfo;
  breeds = new Array<DogBreedInfo>();

  constructor(private auth: AuthService, public dialog: MatDialog, private adoptionService: AdoptionService,
    private userPetService: UserPetService, private petInfoService: PetInfoService) { }

  ngOnInit(): void {
    this.auth.userProfile$.subscribe((prfl) => {
      this.profile = prfl;
    });

    this.petInfoService.getAllBreeds().subscribe((breeds) => {
      this.breeds = breeds;
      this.getUserPets();
    });
  }

  OpenAddPetDialog(): void {
    const dialogRef = this.dialog.open(AddPetProfileDialogComponent, {
      width: '500px',
      data: {
        breeds: this.breeds
      }
    });

    dialogRef.afterClosed().subscribe((result: UserPet) => {
      if (result) {
        this.getUserPets();
      }
    });
  }

  selectPet(selectedChange: MatSelectChange): void {
    this.selectedBreed = this.breeds.find(b => b.id === this.selectedUserPet.breedId);
  }

  getUserPets() {
    this.userPetService.getUserPets(this.profile.sub).subscribe((userPets) => {
      this.userPets = userPets;
      if (userPets.length > 0) {
        this.selectedUserPet = userPets[0];
        this.selectedBreed = this.breeds.find(b => b.id === this.selectedUserPet.breedId);
      }
    });
  }

  PutUpForAdoption() {
    const dialogRef = this.dialog.open(AdoptionDetailsDialogComponent, {
      width: '500px'
    });

    dialogRef.afterClosed().subscribe((result: AdoptionAdditionalDetails) => {
      if (result) {
        let adoption: Adoption = {
          additionalRequirements: result.additionalRequirements,
          adopteeId: this.profile.sub,
          breedName: this.selectedBreed.name,
          fees: result.fees,
          petId: this.selectedUserPet.id,
          petName: this.selectedUserPet.name,
          status: 'Available'
        }
        this.adoptionService.createAdoption(adoption).subscribe((adoption) => {
          // TODO: get adoptions for user again. This should disable buttons based on the status of the adoption
          console.log(adoption);
        });
      }
    });
  }
}
