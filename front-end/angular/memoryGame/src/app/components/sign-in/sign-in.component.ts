import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Global, UserService, stringValidatorArr, numberValidatorArr } from '../../imports';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent {

  //----------------PROPERTIRS-------------------

  signInFormGroup: FormGroup;
  isUniqueUser: boolean = true;
  //allow access 'Object' type via interpolation
  objectHolder: typeof Object = Object;

  //----------------CONSTRUCTOR------------------

  constructor(private formBuilder: FormBuilder, private userService: UserService, private router: Router) {
    this.signInFormGroup = this.formBuilder.group({
      userName: ['', stringValidatorArr('userName', 2, 10, /^[A-Za-z]+$/)],
      age: ['', numberValidatorArr('age', 18, 120)],
      myColor: ['', Validators.required],
    });
  }

  //----------------METHODS-------------------

  onSubmit() {
    this.signIn();
  }

  signIn() {
    this.userService.signIn(this.userName.value, this.age.value)
      .subscribe((created: boolean) => {
        if (created) {
          sessionStorage.setItem(Global.USER,this.userName.value);
          sessionStorage.setItem(Global.MYCOLOR,this.myColor.value);
          this.router.navigate(['/memoryGame/choosePartner']);
        }
      },
        err => {
          if (err.error == 'isNotUnique')
            this.isUniqueUser = false;
          else
            console.log(err);
        });
  }

  //----------------GETTERS-------------------

  //getters of the form group controls

  get userName() {
    return this.signInFormGroup.controls['userName'];
  }
  get age() {
    return this.signInFormGroup.controls['age'];
  }
  get myColor() {
    return this.signInFormGroup.controls['myColor'];
  }
  
}
