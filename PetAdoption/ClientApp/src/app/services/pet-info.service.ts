import { Injectable } from '@angular/core';
import { HttpClient, HttpBackend } from '@angular/common/http';
import {DogBreedInfo} from '../models/dogBreedInfo';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class PetInfoService {

  baseUrl = 'http://localhost:5002/api/doginfo';

  constructor(private http: HttpClient, private handler: HttpBackend) { }

  getAllBreeds(): Observable<Array<DogBreedInfo>> {
    let httpClient = new HttpClient(this.handler);
    return httpClient.get<Array<DogBreedInfo>>(`${this.baseUrl}/getbreeds`);
  }

  searchBreeds(searchStr: string): Observable<Array<DogBreedInfo>> {
    let httpClient = new HttpClient(this.handler);
    return httpClient.get<Array<DogBreedInfo>>(`${this.baseUrl}/searchbreed/${searchStr}`);
  }
}
