import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/user';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  // because we are storing our current user in accountService we need to inject the instance of account Service
  // and our current User is an Observable
  constructor(private accountService: AccountService) {}

  // Interceptors are a unique type of Angular Service that we can implement. Interceptors allow us to intercept incoming or outgoing 
  // HTTP requests using the HttpClient . By intercepting the HTTP request, we can modify or change the value of the request.
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let currentUser!: User;
    //since our currentuser inside an observable we need to subscribeit and take out that current user
    // store it into currentUser we declared above
    
    // --- we have pipe instead of subscribe because it will automatiaclly take care of unsubscribing from
    // observable, and we will use tak(1) 

    this.accountService.currentUser$.pipe(take(1)).subscribe(user => currentUser = user);
    if(currentUser)
    {
      request = request.clone({
        setHeaders:{
          Authorization: `Bearer ${currentUser.token}`

        }
      })
    }
  

    return next.handle(request);
  }
}
