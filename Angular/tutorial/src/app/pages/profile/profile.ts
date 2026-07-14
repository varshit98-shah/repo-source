import { Component, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-profile',
  imports: [],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile {
  all_data = signal({id:0,name:'',age:0});
  constructor(public route:ActivatedRoute){
  }
  ngOnInit(){
    this.route.queryParams.subscribe((params)=>{
      console.log(params)
      this.all_data.set({id:params['id'],name:params['name'],age:params['age']})
    })
  }
}
