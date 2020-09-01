import { Component, OnInit } from '@angular/core';
import { AdoptionService } from '../services/adoption.service';
import { Observable } from 'rxjs';
import { PetInfoService } from '../services/pet-info.service';
import { DogBreedInfo } from '../models/dogBreedInfo';
import { AdoptionExtended } from '../models/adoptionExtended';
import { AdoptionStatus } from '../models/adoptionStatus';
import { Constants } from '../models/Constants';
import { AuthService } from '../services/auth.service';
import { AdopterDetailService } from '../services/adopter-detail.service';
import { UserProfile } from '../models/userProfile';
import { Adoption } from '../models/adoption';
import { AdopterDetail } from '../models/adopterDetail';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs/operators';
import { MatDialog } from '@angular/material/dialog';
import { AdoptPetComponent } from '../adopt-pet/adopt-pet.component';
import { AdopterDetailsDialogData } from '../models/adopterDetailsDialogData';

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
  sessionStorageKey: string = 'adoption-to-initiate';

  constructor(private auth: AuthService, private adoptionService: AdoptionService,
    private petInfoService: PetInfoService, private activatedRoute: ActivatedRoute,
    private dialog: MatDialog, private adopterDetailService: AdopterDetailService) { }

  ngOnInit(): void {
    this.auth.userProfile$.subscribe((prfl) => {
      this.profile = prfl;
      Observable.forkJoin([
        this.petInfoService.getAllBreeds(),
        this.adoptionService.getAdoptions()]).subscribe(results => {
          this.breeds = results[0];
          this.adoptions = results[1];
          this.setAdoptionStatuses();
          this.activatedRoute.params.pipe(map((p: any) => p.action)).subscribe((param) => {
            if (param === 'initiate') {
              let adoptionStr = sessionStorage.getItem(this.sessionStorageKey);
              let adoption = <Adoption>JSON.parse(adoptionStr);

              if (adoption.adopteeId!==this.profile.sub){
                this.adopt(adoption);
              }
              sessionStorage.removeItem(this.sessionStorageKey);
            }
          });
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
      this.setAdoptionStatuses();
    });
  }

  adopt(adoption: Adoption) {
    if (!this.auth.loggedIn) {
      sessionStorage.setItem(this.sessionStorageKey, JSON.stringify(adoption));
      this.auth.login('/adoptions/initiate');
    } else {
      const dialogRef = this.dialog.open(AdoptPetComponent, {
        width: '500px',
        data: this.profile
      });

      dialogRef.afterClosed().subscribe((result: AdopterDetailsDialogData) => {
        if (result) {
          let adopterDetails: AdopterDetail = {
            address: result.address,
            adoptionId: adoption.id,
            givenName: result.givenName,
            lastName: result.lastName,
            telephone: `${result.telephone.area}-${result.telephone.exchange}-${result.telephone.subscriber}`,
            userId: result.userId,
            userPetId: adoption.userPetId
          };
          this.adopterDetailService.createAdopterDetails(adopterDetails).subscribe((savedAdopterDetails) => {
            this.getAllAdoptions();
          });
        }
      });
    }
  }
}
