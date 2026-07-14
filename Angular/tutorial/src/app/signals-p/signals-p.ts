import { Component, computed, effect, signal } from '@angular/core';

@Component({
  selector: 'app-signals-p',
  imports: [],
  templateUrl: './signals-p.html',
  styleUrl: './signals-p.css',
})
export class SignalsP {

  count = 0; //this is for normal property 

  data = signal(0); // this is for signal

  constructor() {
    effect(() => {

      //console.log("This is property count", this.count);
      console.log("Signal Data:", this.data());
    if (this.data() === 10) {
      this.data.set(0);
    }
  });

  effect(()=>{
    console.log("This is computed signal z", this.area());
  });
  }

  countinc() {

    this.count++;
    // console.log(this.count);


  }

  signal() {
    this.data.set(this.data() + 1)
  }

  // computed signal and computed signal functiom
  x = signal(10);
  y = signal(20);
  z = computed(() => this.x() + this.y());


  compute(){
    this.x.set(this.x() + 50);
  }

  // constructor(){

  //   effect(()=>{
  //     console.log("This is computed signal z", this.area());
  //   })
  // }


  //height width example
  height = signal(100);
  widht = signal(10);
  area = computed(()=> this.height() * this.widht());

  computeArea(){
    this.height.set(this.height() + 10);
  }

}
