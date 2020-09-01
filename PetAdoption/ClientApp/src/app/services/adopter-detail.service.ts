import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AdopterDetail } from '../models/adopterDetail';

@Injectable({
  providedIn: 'root'
})
export class AdopterDetailService {

  constructor(private http: HttpClient) { }

  createAdopterDetails(adopterDetails: AdopterDetail){
    return this.http.post<AdopterDetail>(`/api/adopterdetail`, adopterDetails);
  }
}
