import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;

  constructor() { }

  ngOnInit(): void {
  }

  //Changes the registration status on calling
  registerToggle()
  {
    this.registerMode = !this.registerMode;
  }

  //Function to cancel registration
  cancelRegisterMode(event:boolean)
  {
    this.registerMode = event;
  }

}
