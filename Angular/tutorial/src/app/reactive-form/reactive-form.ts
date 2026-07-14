import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

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
    name:new FormControl(''),
    email:new FormControl(''),
    password:new FormControl('')
  })
  
  submitform(){
    console.log(this.loginform.value);
  }
}
