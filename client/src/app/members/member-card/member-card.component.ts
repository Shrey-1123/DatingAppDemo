import { NONE_TYPE } from '@angular/compiler';
import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css'],
  //encapsulation: none;// this will enacapsulate our css card component se that it wont affect other componenet in case ut changes
})
export class MemberCardComponent implements OnInit {

  @Input()
  member!: Member;

  constructor(private memberService: MembersService, private toastr: ToastrService) { 
    
  }

  ngOnInit(): void {
  }

  addLike(member: Member){

    this.memberService.addLike(member.username).subscribe(()=>{
      this.toastr.success('You have liked' + member.knownAs);
    })
  }

}
