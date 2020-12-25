import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment'
import { UserDetails } from '../models/userDetails';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loginPath: string = environment.apiUrl + 'identity/login';
  private registerPath: string = environment.apiUrl + 'identity/register';
  private detailsPath: string = environment.apiUrl + 'identity/details';

  constructor(private http: HttpClient) {

   }

   login(data): Observable<any> {
     return this.http.post(this.loginPath, data);
   }

   register(data): Observable<any> {
    return this.http.post(this.registerPath, data);
  }

  getDetails(): Observable<UserDetails> {
    return this.http.get<UserDetails>(this.detailsPath);
  }

  setDetails(data): Observable<any> {
    return this.http.post(this.detailsPath, data);
  }

  saveToken(token) {
    localStorage.setItem('token', token);
  }

  getToken(){
    return localStorage.getItem('token');
  }

  isAuthenticated() : boolean{
    return this.getToken() !== null;
  }
}
