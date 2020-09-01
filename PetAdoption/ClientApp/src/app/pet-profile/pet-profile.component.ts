import { Component, OnInit } from '@angular/core';
import { DogBreedInfo } from '../models/dogBreedInfo';
import { PetInfoService } from '../services/pet-info.service';
import { Observable } from 'rxjs';
import { UserPet } from '../models/userPet';
import { AuthService } from '../services/auth.service';
import { UserProfile } from '../models/userProfile';
import { UserPetService } from '../services/user-pet.service';
import { AdoptionService } from '../services/adoption.service';
import { MatDialog } from '@angular/material/dialog';
import { AddPetProfileDialogComponent } from '../add-pet-profile-dialog/add-pet-profile-dialog.component';
import { AdoptionDetailsDialogComponent } from '../adoption-details-dialog/adoption-details-dialog.component';
import { AdoptionAdditionalDetails } from '../models/adoptionAdditionalDetails';
import { Adoption } from '../models/adoption';
import { UserPetExtended } from '../models/userPetExtended';
import 'rxjs/add/observable/forkJoin'
import { AdoptionStatus } from '../models/adoptionStatus';
import { Constants } from '../models/Constants';

@Component({
  selector: 'app-pet-profile',
  templateUrl: './pet-profile.component.html',
  styleUrls: ['./pet-profile.component.scss']
})
export class PetProfileComponent implements OnInit {
  profile: UserProfile;
  userPets = new Array<UserPetExtended>();
  selectedUserPet: UserPetExtended;
  selectedBreed: DogBreedInfo;
  breeds = new Array<DogBreedInfo>();
  adoptions = new Array<Adoption>();
  Constants = Constants;

  constructor(private auth: AuthService, public dialog: MatDialog, private adoptionService: AdoptionService,
    private userPetService: UserPetService, private petInfoService: PetInfoService) { }

  ngOnInit(): void {

    this.auth.userProfile$.subscribe((prfl) => {
      this.profile = prfl;
      Observable.forkJoin([
        this.petInfoService.getAllBreeds(),
        this.userPetService.getUserPets(this.profile.sub),
        this.adoptionService.getAdoptionsForUser(this.profile.sub)]).subscribe(results => {
          this.breeds = results[0];
          this.userPets = results[1];
          this.adoptions = results[2];
          this.selectDefaultPetBreed();
          this.setAdoptionStatuses();
        });
    });
  }

  setAdoptionStatuses() {
    this.adoptions.forEach(adoption => {
      var userPet = this.userPets.find(us => us.id === adoption.petId);
      if (userPet) {
        userPet.adoption = adoption;
        switch (adoption.status) {
          case AdoptionStatus.Available:
            userPet.adoptionStatus = Constants.Available;
            break;
          case AdoptionStatus.Closed:
            userPet.adoptionStatus = Constants.Closed;
            break;
          case AdoptionStatus.Pending:
            userPet.adoptionStatus = Constants.Pending;
            break;
          default:
            break;
        }
      }
    });
  }

  selectDefaultPetBreed() {
    if (this.userPets.length > 0) {
      this.selectedUserPet = this.userPets[0];
      this.selectedBreed = this.breeds.find(b => b.id === this.selectedUserPet.breedId);
    }
  }

  openAddPetDialog(): void {
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

  selectPet(): void {
    this.selectedBreed = this.breeds.find(b => b.id === this.selectedUserPet.breedId);
  }

  getUserPets() {
    this.userPetService.getUserPets(this.profile.sub).subscribe((userPets) => {
      this.userPets = userPets;
      this.selectDefaultPetBreed();
    });
  }

  putUpForAdoption() {
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
          status: AdoptionStatus.Available
        }
        this.adoptionService.createAdoption(adoption).subscribe(() => {
          this.adoptionService.getAdoptionsForUser(this.profile.sub).subscribe((adoptions) => {
            this.adoptions = adoptions;
            this.setAdoptionStatuses();
          })
        });
      }
    });
  }

  RemoveAdoption() {
    this.adoptionService.deleteAdoption(this.profile.sub, this.selectedUserPet.adoption.id).subscribe(() => {
      this.adoptionService.getAdoptionsForUser(this.profile.sub).subscribe((adoptions) => {
        this.adoptions = adoptions;
        this.selectedUserPet.adoption = null;
        this.selectedUserPet.adoptionStatus = null;
        this.setAdoptionStatuses();
      });
    });
  }
}
