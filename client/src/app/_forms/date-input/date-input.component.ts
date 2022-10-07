import { Component, Injectable, Input, OnInit, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker/bs-datepicker.config';
import {AppModule} from '../../app.module'
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-date-input',
  templateUrl: './date-input.component.html',
  styleUrls: ['./date-input.component.css']
})

// @Injectable({ providedIn: 'app' })
export class DateInputComponent implements ControlValueAccessor {
  @Input() label:string;
  @Input() minDate:Date;
  bsConfig:Partial<BsDatepickerConfig>;

  constructor(@Self() public ngControl:NgControl)
  {
    this.ngControl.valueAccessor = this;
    this.bsConfig = {
      containerClass: 'theme-default',
      dateInputFormat:'DD MMMM YYYY'
    }
  }

  writeValue(obj: any): void {

    }
    registerOnChange(fn: any): void {

    }
    registerOnTouched(fn: any): void {

    }
    setDisabledState?(isDisabled: boolean): void {

    }

  ngOnInit(): void {
  }

}
