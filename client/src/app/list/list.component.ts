import { Component, OnInit } from '@angular/core';
import { Member } from '../_models/member';
import { Pagination } from '../_models/pagination';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  members: Partial<Member[]>;
  predicate = 'setAppointment';
  pageNumber = 1;
  pageSize = 5;
  pagination: Pagination;


  constructor(private memberService: MembersService) { }

  ngOnInit(): void {
    this.loadAppointments();
  }
  


  loadAppointments()
  {
    this.memberService.getAppointments(this.predicate).subscribe(
      response => {
        this.members = response;
      }
    )
  }

  pageChanged(event: any) {
    this.pageNumber = event.page;
    this.loadAppointments();
  }
}
