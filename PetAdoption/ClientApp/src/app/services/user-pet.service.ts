import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {UserPet} from '../models/userPet';
import { Observable } from 'rxjs';
import { SpaConfigService } from './spa-config.service';

@Injectable({
  providedIn: 'root'
})
export class UserPetService {
  baseUrl: string;

  constructor(private http: HttpClient, private spaConfigService: SpaConfigService) { 
    this.baseUrl = `${this.spaConfigService.spaConfig.userPetInfoApiBaseUrl}/api`;
  }

  createUserPet(userPet: UserPet): Observable<UserPet> {
    return this.http.post<UserPet>(`${this.baseUrl}/userpet`, userPet);
  }

  getUserPets(userId: string): Observable<Array<UserPet>>{
    return this.http.get<Array<UserPet>>(`${this.baseUrl}/user/${userId}/userPets`,)
  }
}
