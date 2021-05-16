import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  users : any; // we are gonna pass this users from home component to register componenet using @Input


  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    //this,this.getUsers();
  }

    registerToggle(){

      this.registerMode = !this.registerMode;
    }

  // getUsers()
  // {
  //   this.http.get('https://localhost:5001/api/users').subscribe(users=>this.users = users); // we dont need {} as this contains only single statement
  // }

  cancelRegisterMode(event:boolean)
  {
    this.registerMode = event; // we are revieving event from registercomponenet as false;
  }
}
