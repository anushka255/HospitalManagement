import { Component, OnInit } from '@angular/core';import { User } from '../_models/user';
import {AccountService} from '../_services/account.service';
import {map} from 'rxjs/operators';
import {Router} from '@angular/router'
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'app-loginhome',
  templateUrl: './loginhome.component.html',
  styleUrls: ['./loginhome.component.css']
})
export class LoginhomeComponent implements OnInit {
  currentUser$: Observable<User>;

  constructor(public accountService: AccountService, private route: Router) { }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
  }


}
