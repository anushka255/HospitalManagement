import { Component, Input, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
import { Pagination } from 'src/app/_models/pagination';
import { UserParams } from 'src/app/_models/userParams';
import { User } from 'src/app/_models/user';
import { NgModel } from '@angular/forms';



@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {
  
  @Input() ngModel: NgModel
  members: Member[];
  pagination: Pagination;
  userParams: UserParams;
  user: User;
  genderList =[{value:'male', display: 'Males'}, {value: 'female', display: 'Females'}]
  userTypeList =[{value:'Patient', display: 'Patient'}]


  constructor(private memberService: MembersService) {
    this.userParams = this.memberService.getUserParams();
  }

  ngOnInit(): void {
    this.loadMembers();
    console.log(this.members);
  }

  loadMembers()
  {
    this.memberService.setUserParams(this.userParams);
    this.memberService.getMembers(this.userParams).subscribe(response => {
      console.log(response);
      this.members = response.result;
      this.pagination = response.pagination;
    })
  }

  resetFilters()
  {
    this.userParams = this.memberService.resetUserParams();
    this.loadMembers();
  }


  pageChanged(event: any) {
    this.userParams.pageNumber = event.page;
    this.memberService.setUserParams(this.userParams);
    this.loadMembers();
  }


}
