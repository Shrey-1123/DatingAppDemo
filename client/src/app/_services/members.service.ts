import { HttpClient} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

// const httpOptions = {
//   headers: new HttpHeaders({
//     Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user') || '{}')?.token
//   })
// } since we have used jwt interceptors we dont need to send http token request header everytime we make request

// service is also used to store state , state mamngement
@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[]=[];
  constructor(private http: HttpClient) { }

  getMembers(): Observable<Member[]>{
    if(this.members.length>0)
    {
      // of is used to return something from an observable
      return of(this.members);
    }
    // map also return an observable
    return this.http.get<Member[]>(this.baseUrl+'users').pipe(
      map((members:any) =>{
        this.members = members;
        return members;
      })
    );
  }

  getMember(username:string){
    const member = this.members.find(x=>x.username===username);
    if(member !== undefined)
    {
      return of(member);
    }
    return this.http.get<Member>(this.baseUrl+'users/'+username);
  }

  updateMember(member: Member){
    
    return this.http.put(this.baseUrl+'users',member).pipe(
      map(()=>{ // we dont have anthing coming rom server so ()
        const index = this.members.indexOf(member);
        this.members[index] = member;
      })
    );
  }
 
}
