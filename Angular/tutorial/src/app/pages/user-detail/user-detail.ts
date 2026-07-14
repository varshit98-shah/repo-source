import { Component, signal } from '@angular/core';
import { Users } from '../../services/users';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-detail',
  imports: [],
  templateUrl: './user-detail.html',
  styleUrl: './user-detail.css',
})
export class UserDetail {
  userdata = signal<any>(null);
  constructor(public user:Users,public route:ActivatedRoute){

  } 
  ngOnInit(){
    const data = this.user.users()
    this.route.params.subscribe((params)=>{
      // console.log(params['id'])
      const fulldata = data.filter((user)=>user.id==params['id'])
    this.userdata.set(fulldata[0])
    })
  } 
}
