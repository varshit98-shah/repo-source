import { Component,signal } from '@angular/core';
import { FormBuilder, FormsModule } from '@angular/forms';

@Component({
  selector: 'app-two-way-binding',
  imports: [FormsModule],
  templateUrl: './two-way-binding.html',
  styleUrl: './two-way-binding.css',
})
export class TwoWayBinding {
  name = signal("Nikhil Barad");
  Name = signal("Nikhil Barad");

  age:number = 20;

  users = signal({
    name:"Nikhil Barad",
    age: 22,
    email: "nikhil@test.com"
  })

  namewithobject = signal({
    name: "Nikhil Barad",
    age :22,
    email:"nikhil@test.com"
  })

  change(name:string,val:string){
    // short version of using the key by making use of [] bracket it takes dynamic value of each input field comes 
    this.users.update((user)=>({...user, [name]:val}))

  }

  // signal get set 
  get name1(){
    return this.Name();
  }
  set name1(val:string){
    this.Name.set(val);
  }


  get objname(){
    return this.namewithobject().name;
  }
  set objname(val:string){
    this.namewithobject.update((user)=>({...user,name:val}))
  }

  get objemail(){
    return this.namewithobject().email;
  }
  set objemail(val:string){
    this.namewithobject.update((user)=>({...user,email:val}))
  }



}
