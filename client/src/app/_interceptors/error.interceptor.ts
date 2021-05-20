import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router:Router, private toastr: ToastrService) {}

  // Interceptors are a unique type of Angular Service that we can implement. Interceptors allow us to intercept incoming or outgoing 
  // HTTP requests using the HttpClient . By intercepting the HTTP request, we can modify or change the value of the request.
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error=>{
        if(error){
          switch(error.status)
          {
            case 400:
              if(error.error.errors){
                const modalStateErrors = [];
                for(const key in error.error.errors){
                  if(error.error.errors[key])
                  {
                    modalStateErrors.push(error.error.errors[key])
                  }
                }

                throw modalStateErrors.flat(); // flat method will concatenate all subarray inside the given array, test validation 400 error 
              }
              else{
                this.toastr.error(error.statusText,error.status);
              }
              break;
              case 401:
                this.toastr.error(error.statusText,error.status);
                break;
               case 404:
                 this.router.navigateByUrl('/not-found');
                 break;
                case 500:
                  const navigationExtras : NavigationExtras ={state:{error:error.error}} // when component load it will hold the error
                  this.router.navigateByUrl('/server-error',navigationExtras);
                  break;  
              default:
                this.toastr.error('Something unexpected went wrong');
                console.log(error);
                break;
          }
        }

        return throwError(error);
      })
      )
  }
}
