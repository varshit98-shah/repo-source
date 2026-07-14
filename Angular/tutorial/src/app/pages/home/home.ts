import { Component, signal, ViewChild, ViewContainerRef } from '@angular/core';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [RouterLink],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
  constructor(public router:Router){
  }
  goprofile(){
    this.router.navigate(['/profile'])
  }

  data= signal({id:1,name:'Nikhil Barad',age:22});

  goprofilequery(){
    this.router.navigate(['profile'],
      {queryParams:{
        id:2,
        name:'Patrick Batman',
        age:30
      }})
  }

  goabout(){
    this.router.navigate(['about','Juliet Nicholas'])
  }

  @ViewChild('container',{read:ViewContainerRef})
  container!:ViewContainerRef;
  async containerexample(){
    this.container.clear();
    console.log("Hello"); 
    const {ContainerExampleUser} = await import('../container-example-user/container-example-user')
    this.container.createComponent(ContainerExampleUser); 
  }
}
