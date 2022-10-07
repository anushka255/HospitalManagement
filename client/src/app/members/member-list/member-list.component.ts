import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
import { AccountService } from 'src/app/_services/account.service';
import { Observable } from 'rxjs/internal/Observable';
import {Triage} from 'src/app/_models/triage';
import { Pagination } from 'src/app/_models/pagination';
import { UserParams } from 'src/app/_models/userParams';
import { take } from 'rxjs/internal/operators/take';
import { User } from 'src/app/_models/user';
import { EmployeeParams } from 'src/app/_models/employeeParams';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  members: Member[];
  allMembers: Member[];
  pagination: Pagination;
  userParams: UserParams;
  user: User;
  employeeParams: EmployeeParams;
genderList =[{value:'male', display: 'Males'}, {value: 'female', display: 'Females'}]
  userTypeList =[
    {value: 'Accounts', display: 'Accounts'},
    {value:'Administration', display: 'Administration'},
    {value:'Clinician', display: 'Clinician'},
    {value:'Physician', display: 'Physician'},
    {value:'Receptionist', display: 'Receptionist'}]

  constructor(private memberService: MembersService) {
    this.userParams = this.memberService.getUserParams();
    this.employeeParams = this.memberService.getEmployeeParam();
  }

  ngOnInit(): void {
    this.loadMembers();
    console.log(this.members);
  }

  loadMembers()
  {
    this.memberService.setEmployeeParam(this.employeeParams);
    this.memberService.getEmployee(this.employeeParams).subscribe(response => {
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
