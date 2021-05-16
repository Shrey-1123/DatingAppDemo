import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { async, Observable, of, Subscriber } from 'rxjs';
import { subscribeOn } from 'rxjs/operators';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

   model:any={}
 //   loggedIn: boolean = false;  we will use this property to check whether user logged or not in NAV 
 // we will use this property to check whether user logged or not in NAV 
   constructor(public accountService:AccountService, private router: Router, private toastr: ToastrService) {
       
   }

  

  ngOnInit(): void {
      if(localStorage.key(0)==null)
      {
        this.logout();
      }
  }

    login(){
      this.accountService.login(this.model).subscribe(response=>{
        this.router.navigateByUrl('/members'); // on successful login page will be navigated to members componenet
        console.log(response);
      
       
      
      },error=>{
        console.log(error);
        this.toastr.error(error.error); 
      })
    }

    logout(){
      this.accountService.logout();
      this.router.navigateByUrl('/');
     
    }

 
}
