import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']

})
export class AppComponent implements OnInit{ // Angular comes with lifecycle event that takes place after constructor is intializd, to use that lifecycle event we need to impllement Interface Onintit
  title = 'The Dating App'; // this is called interpolation i.e we can direclty pass data from our agnular componenet to our view.
  users : any; // to turn off type safety, our users can be of any type such as string, numbers, date etc. 

  constructor(private http: HttpClient){} // just the way we did DI in our startup class, we use contructor to add httpclient service that we created in app.module.ts and then injected here
  
  ngOnInit() {
    // we need to use this keyword to access any poperty of this class such as title,users , http
    // so when we get http response from the endpoint we need to use subscribe as get method is lazy and 
    // wont do anyhting unless subscribed
    // within subscribe we need define what kind of respone we are expecting
    // since we get a list of users, we used arrow function to specify observable return type
    // which is of this.users type and then if we don't get response we specify error and print it to console
    this.getUsers();
  }

  ///HttpClient is used to make Http Request

    getUsers(){
      this.http.get('https://localhost:5001/api/users').subscribe(response => {
        this.users = response;
  
      },error =>{
        console.log(error);
      })
    }
}
