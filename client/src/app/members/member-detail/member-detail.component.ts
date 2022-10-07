import { Component, OnInit, ViewChild } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { Triage } from 'src/app/_models/triage';
import { Billing } from 'src/app/_models/billing';
import { Medication } from 'src/app/_models/medication';
import { Test } from 'src/app/_models/test';
import { Photo } from 'src/app/_models/photo';
import { GalleryItem, ImageItem } from 'ng-gallery';
import { TabsetComponent } from 'ngx-bootstrap/tabs/tabset.component';


@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  @ViewChild('memberTabs', {static: true}) memberTabs: TabsetComponent;
  member: Member;
  triage : Triage;
  billing: Billing;
  test:Test;
  medication:Medication;
  images: GalleryItem[];

  constructor(private memberService: MembersService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadMember();
    this.images = this.getImages();
  }

  getImages()
  {

    const imageUrls = [];
    for(const photo of this.member.photos)
    {
      imageUrls.push(
        {
          src: photo?.url,
          thumb: photo?.url
        })
    }
    return imageUrls;
  }


  //Function to get user information via username
  loadMember()
  {
    this.memberService.getMember(this.route.snapshot.paramMap.get('username')).subscribe(option =>
    {
      this.member = option;
      this.loadTriage();
      this.loadBilling();
      this.loadTest();
      this.loadMedication();
    })
  }

  //Function to connect to get triage information via user id
  loadTriage()
  {

   this.memberService.getTriage(this.member.id).subscribe(option =>
    {
      this.triage = option;

    })
  }


  //Function to connect to get billing information via user id
  loadBilling()
  {
    this.memberService.getBilling(this.member.id).subscribe(option =>
    {
      this.billing = option;
      console.log("Billing", this.billing);
    })
  }


  //Function to connect to get test information via user id
  loadTest()
  {
    this.memberService.getTest(this.member.id).subscribe(option =>
    {
      this.test = option;
    })
  }

  //Function to connect to get Medication information via user id
  loadMedication()
  {
    this.memberService.getMedication(this.member.id).subscribe(option =>
    {
      this.medication = option;
    })
  }
}
