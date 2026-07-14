import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { minLength, validate } from '@angular/forms/signals';
import { min } from 'rxjs';

@Component({
  selector: 'app-reactive-form',
  imports: [ReactiveFormsModule],
  templateUrl: './reactive-form.html',
  styleUrl: './reactive-form.css',
})
export class ReactiveForm {
  email = new FormControl("");
  password = new FormControl("");

  Submit(){
    console.log(this.email.value,this.password.value);
  }

  loginform =new FormGroup({
    name:new FormControl('',[Validators.required]),
    email:new FormControl('',[Validators.required,Validators.email]),
    password:new FormControl('',[Validators.required,Validators.minLength(6)]),
  })
  
  get name(){
    return this.loginform.get("name"); 
  }

  get email1(){
    return this.loginform.get("email");
  }

  get password1(){
    return this.loginform.get("password");
  }
  submitform(){
    console.log(this.loginform.value);
  }
}
