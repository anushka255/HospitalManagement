import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  registerMode = false;
  registerForm: FormGroup;
  minDate:Date;
  validationErrors:string[];

  constructor(private accountService: AccountService, private toastr: ToastrService,
              private fb: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();
    this.minDate = new Date();
    this.minDate.setFullYear(Date.now())
  }

  initializeForm()
  {
    this.registerForm = this.fb.group(
      {
        usertype: ['', Validators.required],
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        username: ['', Validators.required],
        email: ['', Validators.required],
        gender: ['', Validators.required],
        dateOfBirth: ['', Validators.required],
        city: ['', Validators.required],
        country: ['', Validators.required],
        ssn: ['', [Validators.required,Validators.minLength(10), Validators.maxLength(10)]],
        password: ['',[Validators.required,Validators.minLength(4), Validators.maxLength(16)]],
        confirmPassword:['',[Validators.required, this.matchValues('password')]]

      }
    )
    this.registerForm.controls.password.valueChanges.subscribe(() => {
      this.registerForm.controls.confirmPassword.updateValueAndValidity();
    })
  }

  matchValues(matchTo: string): ValidatorFn
  {
    return(control:AbstractControl)=>{
      return control?.value === control?.parent?.controls[matchTo].
        value? null: {isMatching: true}
    }
  }
  
  register()
  {
    this.accountService.register(this.registerForm.value).subscribe(response =>
    {
      this.router.navigateByUrl('/loginhome');
      console.log(response);
      this.cancel();
    },error =>
    {
      this.validationErrors = error;
      console.log(error);
      this.toastr.error(error.error);
    })
  }

  cancel()
  {
    this.cancelRegister.emit(false);
  }


}
