import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {DogBreedInfo} from '../models/dogBreedInfo';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class PetInfoService {

  baseUrl = 'http://localhost:5002/api/doginfo';

  constructor(private http: HttpClient) { }

  getAllBreeds(): Observable<Array<DogBreedInfo>> {
    return this.http.get<Array<DogBreedInfo>>(`${this.baseUrl}/getbreeds`);
  }

  searchBreeds(searchStr: string): Observable<Array<DogBreedInfo>> {
    return this.http.get<Array<DogBreedInfo>>(`${this.baseUrl}/searchbreed/${searchStr}`);
  }
}
