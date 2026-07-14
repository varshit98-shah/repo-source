import { Component, signal } from '@angular/core';
import { AuthService } from '../../auth-service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  constructor(private authService:AuthService){

  }
  loginForm = new FormGroup({
    email: new FormControl('',[Validators.required,Validators.email]),
    password: new FormControl('',[Validators.required,Validators.minLength(6)])
  });
  onlogin(){
    if(this.loginForm.invalid){
      this.loginForm.markAllAsTouched();
      return;
    }
    this.authService.login(this.loginForm.value)
    .subscribe({
      next:(response:any)=>{
        console.log(response);

        localStorage.setItem('token',response.token);
        alert('Login Succesful')
      },
      error:(error)=>{
        console.log(error);
        alert('Invalid Credintials')
      }
    });

  }
}
