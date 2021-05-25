import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/user';
import { UserParams } from 'src/app/_models/userParams';
import { AccountService } from 'src/app/_services/account.service';
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
 userParams:UserParams;
 user:User;
 genderList = [{value: 'male', display: 'Males'}, {value: 'female', display: 'Female'}]

  constructor(private memberService: MembersService) { 

    this.userParams = this.memberService.getUserParmas(); // memberServce ka userParams mien filter cache kiya h islie hmlog uska service use kr rhe h rather than accountService 
  }

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
   this.memberService.setUserParams(this.userParams);
   this.memberService.getMembers(this.userParams).subscribe(response=>{
     this.members = response.result;
     this.pagination = response.pagination;
     
   })
 }
 
 resetFilters(){

  //  this.userParams = new UserParams(this.user);
  this.userParams = this.memberService.resetUserParams();
   this.loadMembers();
 }
 pageChanged(event: any)
 {
   this.userParams.pageNumber = event.page; // this will change current page number
   this.memberService.setUserParams(this.userParams);
   this.loadMembers();
 }

}
