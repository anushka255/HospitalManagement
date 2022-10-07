
import { Component, Injectable, Input, OnInit, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';
import { AppModule } from 'src/app/app.module';


@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.css']
})

@Injectable()
export class TextInputComponent implements ControlValueAccessor {
  @Input() label:string;
  @Input() type = 'text';
  
  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
  }

  writeValue(obj: any): void {
    }
    registerOnChange(fn: any): void {
    }
    registerOnTouched(fn: any): void {
    }


  ngOnInit(): void {
  }

}
