import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import {AccountService} from '../_services/account.service';
import {map} from 'rxjs/operators';
import {Router} from '@angular/router'
import { ToastrService } from 'ngx-toastr';
import {CommonModule} from '@angular/common';
import {MembersService} from '../_services/members.service'

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model:any = {};
  currentUser$: Observable<User>;
  logoUrl = "../../assets/Memorial Hospital -logos/Memorial Hospital -logos_white.png"

  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService){};

  ngOnInit(): void {
    //Connects with account service to retrieve currently logged in user
    this.currentUser$ = this.accountService.currentUser$;
  }

  //Function to login to the applicaiton
  //Navigates to the members page upon success
  //Shows valid errorr response upon failure
  login()
  {
    this.accountService.login(this.model).subscribe(response => {
     console.log(response);
     this.router.navigateByUrl('/loginhome');
    }, error =>
    {
      console.log(error);
    })
  }


  //Function to logout to the applicaiton
  //Uses logout feature of the account service to do so
  //Navigates to home page on success
  logout()
  {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }


}
