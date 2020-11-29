import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment'

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loginPath: string = environment.apiUrl + 'identity/login';
  private registerPath: string = environment.apiUrl + 'identity/register';

  constructor(private http: HttpClient) {

   }

   login(data): Observable<any> {
     return this.http.post(this.loginPath, data);
   }

   register(data): Observable<any> {
    return this.http.post(this.registerPath, data);
  }

  saveToken(token) {
    localStorage.setItem('token', token);
  }

  getToken(){
    localStorage.getItem('token');
  }
}