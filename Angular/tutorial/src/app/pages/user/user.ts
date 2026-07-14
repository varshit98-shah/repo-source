import { Component, signal } from '@angular/core';
import { Users } from '../../services/users';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-user',
  imports: [RouterLink],
  templateUrl: './user.html',
  styleUrl: './user.css',
})
export class User {
  userdata = signal<any[]>([]);
  constructor(public user:Users){

  }
  ngOnInit(){
    const data =this.user.users()
    this.userdata.set(data)
  }

}
