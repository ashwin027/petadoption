import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Adoption } from '../models/adoption';

@Injectable({
  providedIn: 'root'
})
export class AdoptionService {

  constructor(private http: HttpClient) { }

  createAdoption(adoption: Adoption): Observable<Adoption> {
    return this.http.post<Adoption>(`/api/adoption`, adoption);
  }

  getAdoptions(): Observable<Array<Adoption>>{
    return this.http.get<Array<Adoption>>(`api/adoption`);
  }

  getAdoptionsForUser(userId: string): Observable<Array<Adoption>>{
    return this.http.get<Array<Adoption>>(`api/adoption/user/${userId}`);
  }
}
