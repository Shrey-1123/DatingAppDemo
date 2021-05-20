import { NONE_TYPE } from '@angular/compiler';
import { Component, Input, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css'],
  //encapsulation: none;// this will enacapsulate our css card component se that it wont affect other componenet in case ut changes
})
export class MemberCardComponent implements OnInit {

  @Input()
  member!: Member;

  constructor() { 
    
  }

  ngOnInit(): void {
  }

}
