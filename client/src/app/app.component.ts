import { Component, OnInit } from '@angular/core';
import{HttpClient} from '@angular/common/http';
import { AccountService } from './_services/account.service';
import { User } from './_models/user';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
;


@Component({

  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Hospital Management System';
  users:any;

  constructor(private http: HttpClient, private accountService: AccountService)
  {

  }

  ngOnInit(){
      this.setCurrentUser();
  }

  //User that is currently active
  //Retrieved from the local storage
  setCurrentUser()
  {
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }



}
