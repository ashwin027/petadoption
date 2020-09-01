import { Component, OnInit } from '@angular/core';
import { AdoptionService } from '../services/adoption.service';
import { Observable } from 'rxjs';
import { PetInfoService } from '../services/pet-info.service';
import { DogBreedInfo } from '../models/dogBreedInfo';
import { AdoptionExtended } from '../models/adoptionExtended';
import { AdoptionStatus } from '../models/adoptionStatus';
import { Constants } from '../models/Constants';
import { AuthService } from '../services/auth.service';
import { UserProfile } from '../models/userProfile';

@Component({
  selector: 'app-adoptions',
  templateUrl: './adoptions.component.html',
  styleUrls: ['./adoptions.component.scss']
})
export class AdoptionsComponent implements OnInit {
  adoptions = new Array<AdoptionExtended>();
  breeds: Array<DogBreedInfo> = new Array<DogBreedInfo>();
  Constants = Constants;
  profile: UserProfile;
  
  constructor(private auth: AuthService, private adoptionService: AdoptionService, private petInfoService: PetInfoService) { }

  ngOnInit(): void {
    this.auth.userProfile$.subscribe((prfl) => {
      this.profile = prfl;
      Observable.forkJoin([
        this.petInfoService.getAllBreeds(),
        this.adoptionService.getAdoptions()]).subscribe(results => {
          this.breeds = results[0];
          this.adoptions = results[1];
          this.setAdoptionStatuses();
        });
    });
  }

  setAdoptionStatuses() {
    this.adoptions.forEach(adoption => {
      switch (adoption.status) {
        case AdoptionStatus.Available:
          adoption.adoptionStatus = Constants.Available;
          break;
        case AdoptionStatus.Closed:
          adoption.adoptionStatus = Constants.Closed;
          break;
        case AdoptionStatus.Pending:
          adoption.adoptionStatus = Constants.Pending;
          break;
        default:
          break;
      }
    });
  }

  getAllAdoptions() {
    this.adoptionService.getAdoptions().subscribe((adoptions) => {
      this.adoptions = adoptions;
    });
  }

  adopt(){
    
  }

}
