import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
import {NgxGalleryOptions} from '@kolkov/ngx-gallery';
import {NgxGalleryImage} from '@kolkov/ngx-gallery';
import {NgxGalleryAnimation} from '@kolkov/ngx-gallery';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { MessageService } from 'src/app/_services/message.service';
import { Message } from 'src/app/_models/message';


@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  @ViewChild('memberTabs',  {static : true} )memberTabs: TabsetComponent; // hmlog memberTabs  ka decorator define kiye tabset mien kyunki hmien Messages vala tbhi load krna h jb User Messages dekhna cahe, otheriwse hmara API call waste hoga, so hm kuch aisa krenge ki app-member-messages vala componenet tbhi click ho jb user usko usko dekhna cahe, niche ek active tab bhi daala h
  member!: Member;
  galleryOptions: NgxGalleryOptions[]=[];
  galleryImages: NgxGalleryImage[]=[];
  activeTab: TabDirective;
  messages: Message[]=[];

  constructor(private memeberService : MembersService, private route: ActivatedRoute, private messageService: MessageService) { }

  // everyhting inside this will run synchronously i.e one after other and we are not waiting to load our member bfore we set gallery images property
  ngOnInit(): void {
    //this.loadMember();
    this.route.data.subscribe(data=>{
      this.member = data.member;
    })

    this.galleryImages = this.getImages(); 

    this.route.queryParams.subscribe(params=>{
      params.tab ? this.selectTab(params.tab) : this.selectTab(0);
    })

    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false


      }
    ]

    
  }

  getImages(): NgxGalleryImage[]{
    const imageUrls = [];
    for(const photo of this.member.photos)
    {
       imageUrls.push({
         small: photo?.url,
         medium: photo?.url,
         big: photo?.url

       })
    }

    return imageUrls;
  }

  // not using after adding route resolver
  // loadMember(){
  //   // this will got to API and getMember by username it is an observable
  //   this.memeberService.getMember(this.route.snapshot.paramMap.get('username') || '{}').subscribe(member=>{
  //     this.member = member;
  //     //this.galleryImages = this.getImages(); // we set galleryproperty before we load members
  //   })
  // }

  selectTab(tabId: number)
  {
    this.memberTabs.tabs[tabId].active = true;
  }

  onTabActivated(data: TabDirective)
  {
    this.activeTab = data;
    if(this.activeTab.heading === 'Messages' && this.messages.length === 0)
    {
      // this.loadMessages(); we already know that member-message component is a child component of memeber-details 
      //, what we do is bring that loadMessage method of message component here so that we can loadMessages as we want
      this.loadMessages();
    }
  }
  loadMessages()
  {
    this.messageService.getMessageThread(this.member.username).subscribe(messages=>{

      this.messages = messages;

    })
  }


}
