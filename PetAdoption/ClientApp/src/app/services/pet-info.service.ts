import { Injectable } from '@angular/core';
import { HttpClient, HttpBackend } from '@angular/common/http';
import {DogBreedInfo} from '../models/dogBreedInfo';
import { Observable } from 'rxjs/internal/Observable';
import { SpaConfigService } from './spa-config.service';

@Injectable({
  providedIn: 'root'
})
export class PetInfoService {
  baseUrl: string;

  constructor(private http: HttpClient, private handler: HttpBackend, private spaConfigService: SpaConfigService) { 
    this.baseUrl = `${this.spaConfigService.spaConfig.petInfoApiBaseUrl}/api/dogInfo`;
  }

  getAllBreeds(): Observable<Array<DogBreedInfo>> {
    let httpClient = new HttpClient(this.handler);
    return httpClient.get<Array<DogBreedInfo>>(`${this.baseUrl}/getbreeds`);
  }

  searchBreeds(searchStr: string): Observable<Array<DogBreedInfo>> {
    let httpClient = new HttpClient(this.handler);
    return httpClient.get<Array<DogBreedInfo>>(`${this.baseUrl}/searchbreed/${searchStr}`);
  }
}
