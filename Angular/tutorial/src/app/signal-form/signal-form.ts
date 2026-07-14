import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { form, FormField} from '@angular/forms/signals';

@Component({
  selector: 'app-signal-form',
  imports: [CommonModule,FormField],
  templateUrl: './signal-form.html',
  styleUrl: './signal-form.css',
})
export class SignalForm {
  loginmodel =signal({
    name:'',
    email:''

  })

  loginform = form(this.loginmodel)

  login(){
    console.log(this.loginform.name().value());
    console.log(this.loginform.email().value());
    
  }
}
