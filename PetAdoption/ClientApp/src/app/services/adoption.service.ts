import { Injectable } from '@angular/core';
import { HttpClient, HttpBackend } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Adoption } from '../models/adoption';
import { AdopterDetail } from '../models/adopterDetail';

@Injectable({
  providedIn: 'root'
})
export class AdoptionService {

  constructor(private http: HttpClient, private handler: HttpBackend) { }

  createAdoption(adoption: Adoption): Observable<Adoption> {
    return this.http.post<Adoption>(`/api/adoption`, adoption);
  }

  updateAdoption(adoption: Adoption): Observable<Adoption> {
    return this.http.put<Adoption>(`/api/adoption`, adoption);
  }

  updateAdoptionWithDetails(adopterDetails: AdopterDetail): Observable<Adoption> {
    return this.http.put<Adoption>(`/api/adoption/details`, adopterDetails);
  }

  getAdoptions(): Observable<Array<Adoption>>{
    let httpClient = new HttpClient(this.handler);
    return httpClient.get<Array<Adoption>>(`api/adoption`);
  }

  getAdoptionsForUser(userId: string): Observable<Array<Adoption>>{
    return this.http.get<Array<Adoption>>(`api/adoption/user/${userId}`);
  }

  deleteAdoption(userId: string, id: number){
    return this.http.delete(`api/user/${userId}/adoption/${id}`);
  }
}
