import { HttpClient} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

// const httpOptions = {
//   headers: new HttpHeaders({
//     Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user') || '{}')?.token
//   })
// } since we have used jwt interceptors we dont need to send http token request header everytime we make request
@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getMembers(): Observable<Member[]>{
    return this.http.get<Member[]>(this.baseUrl+'users');
  }

  getMember(username:string){
    return this.http.get<Member>(this.baseUrl+'users/'+username);
  }
}
