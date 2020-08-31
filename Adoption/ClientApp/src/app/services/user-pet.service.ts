import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {UserPet} from '../models/userPet';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserPetService {
  baseUrl = 'http://localhost:5001/api/userpet';
  constructor(private http: HttpClient) { }

  createUserPet(userPet: UserPet): Observable<UserPet> {
    return this.http.post<UserPet>(this.baseUrl, userPet);
  }
}
