import { Component, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-about-us',
  imports: [],
  templateUrl: './about-us.html',
  styleUrl: './about-us.css',
})
export class AboutUs {
  users=signal('');
  age = signal(0);
  constructor(public route:ActivatedRoute){

  }
  ngOnInit(){
    this.route.params.subscribe((params)=>{
      console.log(params)
      this.users.set(params['name'])
      this.age.set(params['age'])
    })
  }

}
