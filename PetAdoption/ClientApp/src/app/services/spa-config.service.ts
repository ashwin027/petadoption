import { Injectable } from '@angular/core';
import { HttpClient, HttpBackend } from '@angular/common/http';
import { SpaConfig } from '../models/spaConfig';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SpaConfigService {
  spaConfig: SpaConfig;
  constructor(private handler: HttpBackend) { }

  getSpaConfig(): Promise<SpaConfig> {
    return new Promise((resolve, reject) => {
      let httpClient = new HttpClient(this.handler);
      httpClient.get<SpaConfig>('/api/spaconfig').subscribe((config) => {
        this.spaConfig = config;
        resolve(config);
      });
    });
  }
}
