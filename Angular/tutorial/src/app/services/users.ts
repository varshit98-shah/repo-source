import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class Users {
  users = signal([{
    id:1,
    name:'Nikhil Barad',
    email:'nikhil@test.com',
    age:22
  },
  {
    id:2,
    name:'Julliet Nicholas',
    email:'julliet@test.com',
    age:25
  },
  {
    id:3,
    name:'Knox', 
    email:'knox@test.com',
    age:42
  },
  {
    id:4,
    name:'Patrick Batman',
    email:'patrick@test.com',
    age:30
  },
  {
    id:5,
    name:'Goku son',
    email:'goku@test.com',
    age:1000
  }
])
}
