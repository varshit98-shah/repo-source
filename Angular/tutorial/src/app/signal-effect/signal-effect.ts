import { Component, effect, signal } from '@angular/core';

@Component({
  selector: 'app-signal-effect',
  imports: [],
  templateUrl: './signal-effect.html',
  styleUrl: './signal-effect.css',
})
export class SignalEffect {

  speed = signal(0);
  color = "Black";

  fruits = signal("Apple");

  constructor(){
    effect(()=> {
      if(this.speed()>0 && this.speed()<100){
        this.color ="Green";
      }
      if(this.speed()>=60 && this.speed()<120){
        this.color ="Yellow";
      }
      if(this.speed()>=120){
        this.color ="Red";
      }

      console.log("Speed",this.speed());
      
    })

    effect(()=>{
      console.log(this.fruits()); 
    })
  }

  UpdateSpeed(){
    this.speed.update((value) => value + 10);
    //this.speed.set(this.speed()+ 10);
  }

  ChangeFruit(){
    this.fruits.set("Mango");
  }


}
