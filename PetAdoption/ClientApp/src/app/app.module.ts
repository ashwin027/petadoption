import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MaterialModule} from './material.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ProfileComponent } from './profile/profile.component';
import { PetProfileComponent } from './pet-profile/pet-profile.component';
import { HttpClientModule } from '@angular/common/http';
import { AddPetProfileDialogComponent } from './add-pet-profile-dialog/add-pet-profile-dialog.component';
import { AdoptionsComponent } from './adoptions/adoptions.component';
import { AdoptionDetailsDialogComponent } from './adoption-details-dialog/adoption-details-dialog.component';
import { AdoptPetComponent } from './adopt-pet/adopt-pet.component';
import { TelephoneInputComponent } from './telephone-input/telephone-input.component';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { AdoptionRequestComponent } from './adoption-request/adoption-request.component';

@NgModule({
  declarations: [
    AppComponent,
    ProfileComponent,
    PetProfileComponent,
    AddPetProfileDialogComponent,
    AdoptionsComponent,
    AdoptionDetailsDialogComponent,
    AdoptPetComponent,
    TelephoneInputComponent,
    ConfirmationDialogComponent,
    AdoptionRequestComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    FontAwesomeModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
