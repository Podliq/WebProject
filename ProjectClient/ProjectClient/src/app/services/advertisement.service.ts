import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Advertisement } from '../models/advertisement';

@Injectable({
  providedIn: 'root'
})
export class AdvertisementService {
  private advertisementPath = environment.apiUrl + 'Advertisements';
  constructor(private http: HttpClient) { }

  create(data) : Observable<Advertisement> {
    return this.http.post<Advertisement>(this.advertisementPath, data);
  }

  getAdvertisements() : Observable<Array<Advertisement>> {
    return this.http.get<Array<Advertisement>>(this.advertisementPath);
  }

  getAdvertisement(id: number) : Observable<Advertisement> {
    return this.http.get<Advertisement>(this.advertisementPath + `/` + id);
  }

  delete(id: number){
    return this.http.delete(this.advertisementPath + '/' + id);
  }
}

