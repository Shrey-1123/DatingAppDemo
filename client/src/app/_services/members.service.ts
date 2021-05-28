import { HttpClient, HttpParams} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, pipe } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { PaginatedResult } from '../_models/pagination';
import { User } from '../_models/user';
import { UserParams } from '../_models/userParams';
import { AccountService } from './account.service';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

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
  memeberCache= new Map();
  userParams: UserParams;
  user: User;
  constructor(private http: HttpClient, private accountService: AccountService) { 

    
    this.accountService.currentUser$.pipe(take(1)).subscribe(user=>{

      this.user = user;
      this.userParams = new UserParams(user);
    })
  }
 getUserParmas()
 {
   return this.userParams;
 }

 setUserParams(params: UserParams)
 {
   this.userParams = params;
 }

 resetUserParams()
 {
   this.userParams = new UserParams(this.user);
   return this.userParams;

 }
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
    //console.log(Object.values(userParams).join('-'));
    
    var response  = this.memeberCache.get(Object.values(userParams).join('-'));
    if(response)
    {
      return of(response);
    }
    let params =getPaginationHeaders(userParams.pageNumber, userParams.pageSize);

    params = params.append('minAge', userParams.minAge.toString());
    params = params.append('maxAge', userParams.maxAge.toString());
    params = params.append('gender',userParams.gender);
    params = params.append('orderBy',userParams.orderBy);

    // passing value of Current pageNumber and pageSize to UsersController getMembers method

    /// CAHCHING

    // the main idea of using memberCache is that we need optimise the query slections,
    // our params is inside the userParams adn every time we apply filters a new query is genearted for for each type of new userParams
    // so what we can do when we call a userParams by using say gender property we store it inot a map and then we will use
    // pipe to check if response is already present inside the map then we can return it from map rather than going into database again and again

    // isko simple language mien khe to jb hmlog new page mien jayegnge toh page load hoga and response map mien store h jayega
    // adn then waps se pichle page , by page means paginaion vala page not webpage ta new link, tb hmien dobara API mien nhi jana pdega
    // and hmara pichla members vala page jldi load ho jayega
    return getPaginatedResult<Member[]>(this.baseUrl+'users', params,this.http)
    .pipe(map(response=>{
      this.memeberCache.set(Object.values(userParams).join('-'), response);

      return response;
    }))
  }

  

  getMember(username:string){
    // const member = this.members.find(x=>x.username===username);
    // if(member !== undefined)
    // {
    //   return of(member);
    // }
    // console.log(this.memeberCache); // this will prodcue paginated result into console
    // we see that whenever we log into as a user we get paginatedResult containing list of members,
    // and if get into a profile under matches section we get array of aray of paginated results 
    // so let;s concatenate those array of arrays into a single arrays
    const member = [...this.memeberCache.values()]
                    .reduce((arr,elem)=>arr.concat(elem.result),[]) // [] represent and default intial value of arr
                    .find((member: Member)=>member.username === username);
                
                    if(member)
                    {
                      return of(member);
                    }

                    //console.log(member);
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

  addLike(username:string){
    return this.http.post(this.baseUrl + 'likes/'+ username,{})
  }

  getLikes(predicate:string, pageNumber, pageSize)
  {
    let newparams = getPaginationHeaders(pageNumber, pageSize);
    newparams = newparams.append('predicate', predicate);
    // return this.http.get<Partial<Member[]>>(this.baseUrl+'likes?predicate='+predicate);
    // console.log(newparams);
    // console.log(this.baseUrl);
    return getPaginatedResult<Partial<Member[]>>(this.baseUrl+'likes', newparams,this.http);
  }
 
}
