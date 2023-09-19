import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginServiceService {
  private baseUrl = "https://localhost:7101/api/User"

  constructor(private http:HttpClient) { }

  login(userData:any):Observable<any>{
    const headers = new HttpHeaders({ 'Content-Type':'application/json'});
    return this.http.post(`${this.baseUrl}/Login`, userData, {headers});
  }
}
