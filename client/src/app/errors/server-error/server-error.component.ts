import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.css']
})
export class ServerErrorComponent implements OnInit {
 
  // only inside constructor we can hold state of router not in OnInit
  error:any;
  constructor(private router: Router) { 
    const navigation = this.router.getCurrentNavigation();
    this.error = navigation?.extras?.state?.error;
  }

  ngOnInit(): void {
  }

}
