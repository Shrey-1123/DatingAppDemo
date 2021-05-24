import { HttpClient, HttpParams} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { PaginatedResult } from '../_models/pagination';
import { UserParams } from '../_models/userParams';

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

  // getMembers(): Observable<Member[]>{
  //   if(this.members.length>0)
  //   {
  //     // of is used to return something from an observable
  //     return of(this.members);
  //   }
  //   // map also return an observable
  //   return this.http.get<Member[]>(this.baseUrl+'users').pipe(
  //     map((members:any) =>{
  //       this.members = members;
  //       return members;
  //     })
  //   );
  // }

  getMembers(userParams: UserParams){
    
    let params =this.getPaginationHeaders(userParams.pageNumber, userParams.pageSize);

    params = params.append('minAge', userParams.minAge.toString());
    params = params.append('maxAge', userParams.maxAge.toString());
    params = params.append('gender',userParams.gender);

    // passing value of Current pageNumber and pageSize to UsersController getMembers method
    return this.getPaginatedResult<Member[]>(this.baseUrl+'users', params)
  }

  private getPaginatedResult<T>(url, params)
  {
   const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();

    return this.http.get<T>(this.baseUrl + 'users', { observe: 'response', params }).pipe(
      map(response => {
        //console.log(response);
        // seeting result property of PaginatedResult class to response recieved through API
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') !== null) {
          // we will revieve a string response which ewe need to parse into json
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );
  }

  private getPaginationHeaders(pageNumber: number, pageSize: number){
    let params = new HttpParams();

  
    
       params = params.append('pageNumber', pageNumber.toString());
       params = params.append('pageSize', pageSize.toString());

     return params;
     
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

  setMainPhoto(photoId: number)
  {
    return this.http.put(this.baseUrl+'users/set-main-photo/'+ photoId,{});
  }

  deletePhoto(photoId: number)
  {
    return this.http.delete(this.baseUrl + 'users/delete-photo/'+ photoId);
  }


 
}
