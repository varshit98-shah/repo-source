import { inject, Injectable } from '@angular/core';
import { apiconfig } from '../config/api.config';
// import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);
  private api_url = `${apiconfig.BASE_URL}`

  login(data:any){
    return this.http.post(`${this.api_url}${apiconfig.ENDPOINTS.AUTH.LOGIN}`,data);
  }
  register(data:any){
    return this.http.post(`${this.api_url}${apiconfig.ENDPOINTS.AUTH.REGISTER}`,data)
  }
  


}
