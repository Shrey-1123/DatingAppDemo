import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  // members$!: Observable<Member[]>;
 members:Member[];
 pagination: Pagination;
 pageNumber = 1;
 pageSize = 5;

  constructor(private memberService: MembersService) { }

  ngOnInit(): void {
    //this.members$ = this.memberService.getMembers();
    this.loadMembers();
    
  }
  // loadMembers(){
  //   this.memberService.getMembers().subscribe(members=>{
  //     this.members=members;
  //   })
  // }
  // we will prvide current page number and page sze then we will load the response recieved by the getMembers methods of
  // MemberService and then change the response header as pagination
 loadMembers(){
   this.memberService.getMembers(this.pageNumber, this.pageSize).subscribe(response=>{
     this.members = response.result;
     this.pagination = response.pagination;
     
   })
 }

 pageChanged(event: any)
 {
   this.pageNumber = event.page; // this will change current page number
   this.loadMembers();
 }

}
