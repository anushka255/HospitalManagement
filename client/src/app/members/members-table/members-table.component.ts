import {Component, OnInit} from '@angular/core';
import {Member} from 'src/app/_models/member';
import {MembersService} from 'src/app/_services/members.service';
import {Pagination} from 'src/app/_models/pagination';
import {UserParams} from 'src/app/_models/userParams';
import {User} from 'src/app/_models/user';
import { EmployeeParams } from 'src/app/_models/employeeParams';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-members-table',
  templateUrl: './members-table.component.html',
  styleUrls: ['./members-table.component.css']
})
export class MembersTableComponent implements OnInit {
  members: Member[];
  pagination: Pagination;
  userParams: EmployeeParams;
  user: User;


  constructor(private memberService: MembersService) {
    this.userParams = this.memberService.getEmployeeParam();
  }

  ngOnInit(): void {
    this.loadMembers();
    console.log(this.members);
  }

  loadMembers()
  {
    this.memberService.setEmployeeParam(this.userParams);
    this.memberService.getEmployee(this.userParams).subscribe(response => {
      console.log(response);
      this.members = response.result;
      this.pagination = response.pagination;
    })
  }

  resetFilters()
  {
    this.userParams = this.memberService.resetEmployeeParams();
    this.loadMembers();
  }


  pageChanged(event: any) {
    this.userParams.pageNumber = event.page;
    this.memberService.setEmployeeParam(this.userParams);
    this.loadMembers();
  }


}
