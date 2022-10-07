import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';

@Component({
  selector: 'app-member-select-edits',
  templateUrl: './member-select-edits.component.html',
  styleUrls: ['./member-select-edits.component.css']
})
export class MemberSelectEditsComponent implements OnInit {

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
    this.memberService.getMember(this.route.snapshot.paramMap.get('username')).subscribe(option => {
      this.member = option;
    });

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
