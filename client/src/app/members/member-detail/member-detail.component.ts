import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
import {NgxGalleryOptions} from '@kolkov/ngx-gallery';
import {NgxGalleryImage} from '@kolkov/ngx-gallery';
import {NgxGalleryAnimation} from '@kolkov/ngx-gallery';


@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  member!: Member;
  galleryOptions: NgxGalleryOptions[]=[];
  galleryImages: NgxGalleryImage[]=[];
  constructor(private memeberService : MembersService, private route: ActivatedRoute) { }

  // everyhting inside this will run synchronously i.e one after other and we are not waiting to load our member bfore we set gallery images property
  ngOnInit(): void {
    this.loadMember();

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
  loadMember(){
    // this will got to API and getMember by username it is an observable
    this.memeberService.getMember(this.route.snapshot.paramMap.get('username') || '{}').subscribe(member=>{
      this.member = member;
      this.galleryImages = this.getImages(); // we set galleryproperty before we load members
    })
  }


}
