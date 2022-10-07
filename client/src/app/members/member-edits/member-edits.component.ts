import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import {take} from 'rxjs/operators'
import { User } from 'src/app/_models/user';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-member-edits',
  templateUrl: './member-edits.component.html',
  styleUrls: ['./member-edits.component.css']
})
export class MemberEditsComponent implements OnInit {
  member: Member;
  user: User;

  @ViewChild ('editForm') editForm: NgForm;

  @HostListener('window:beforeunload', ['$event'])unloadNotification($event:any)
  {
    if(this.editForm.dirty)
    {
      $event.returnValue = true;
    }
  }

  constructor(private accountService: AccountService, private memberService: MembersService, private toastr: ToastrService, private route: ActivatedRoute) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user)
  }

  ngOnInit(): void {
    this.loadMember();
  }


  loadMember()
  {
    this.memberService.getMember(this.user.username).subscribe(member =>
    {
      this.member = member;
    })

  }


  updateMember()
  {
    this.memberService.updateMember(this.member).subscribe(() =>
    {
      alert("Your update was successful!");
      console.log(this.member);
      this.editForm.reset(this.member);
    })
  }

}
