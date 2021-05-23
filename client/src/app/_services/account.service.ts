import { HttpClient } from '@angular/common/http';
import { nullSafeIsEquivalent } from '@angular/compiler/src/output/output_ast';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  
  private currentUserSource:ReplaySubject<any>;
  public currentUser$:Observable<User>;
  


  constructor(private http:HttpClient) {
    this.currentUserSource = new ReplaySubject<User>(1); // this will be covered in ROuting
    this.currentUser$ = this.currentUserSource.asObservable();
   }
  /// What we are doing here is, trying to persistning user login
  /// We are using Obsersvable for that, 

  login(model : any)//--> login method takes model of anytype
  {
    // inside this method we are using that model, and we are coverting that model into JSON ody that will be replaced by the post response
    // this login method will return post method
    // We are using Pipe from rxjs and inside pipe we are using map opeartor to check whether 
    // response map with user then we are seeting that response into localstorage


    // Note after changing model type from any to User type we need to cast post into User type.
    return this.http.post<User>(this.baseUrl+'account/login',model).pipe(
      map((response:User)=>{
        const user = response;
        if(user)
        {
          //localStorage.setItem('user',JSON.stringify(user));
          this.SetCurrentUser(user);
          this.currentUserSource.next(user);
          
        }

      })
    )
    
  }
  logout()
  {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    
  }

  SetCurrentUser(user:User)
  {
    localStorage.setItem('user',JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  //register user

  // in this method we are not returning anyhting so whenever we subscribe this method and try to print response in console we will get undefined
  register(model:any)
  {
     return this.http.post<User>(this.baseUrl+'account/register', model).pipe(
      map((user:User)=>{
        if(user)
        {
          //localStorage.setItem('user',JSON.stringify(user));
          this.SetCurrentUser(user);
          this.currentUserSource.next(user);
        }
        //return user;
      })
      )
  }
 
}
