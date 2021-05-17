import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.css']
})
export class TestErrorsComponent implements OnInit {
  baseUrl = 'https://localhost:5001/api/';
  constructor(private http: HttpClient) { }

  //there are certain errors which we cannot check from client side such as username already taken and for that we are gonna need something
  validationErrors: String[] = [];

  ngOnInit(): void {
  }
  
  get404Error(){
    this.http.get(this.baseUrl+'buggy/not-found').subscribe( (response: any)=>{
      console.log(response);
    },error =>{
      console.log(error);
    })
  }
  get500Error(){
    this.http.get(this.baseUrl+'buggy/server-error').subscribe( (response: any)=>{
      console.log(response);
    }, error =>{
      console.log(error);
    })
  }
  get401Error(){
    this.http.get(this.baseUrl+'buggy/auth').subscribe( (response: any)=>{
      console.log(response);
    }, error =>{
      console.log(error);
    })
  }
  get400ValidationError(){
    this.http.post(this.baseUrl+'account/register',{}).subscribe( (response: any)=>{
      console.log(response);
    }, error =>{
      console.log(error);
      this.validationErrors = error;
    })
  }
  get400Error(){
    this.http.get(this.baseUrl+'buggy/bad-request').subscribe( (response: any)=>{
      console.log(response);
    }, error =>{
      console.log(error);
    })
  }

}
