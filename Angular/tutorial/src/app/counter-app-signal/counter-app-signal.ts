import { Component,effect,Signal,signal, WritableSignal } from '@angular/core';

@Component({
  selector: 'app-counter-app-signal',
  imports: [],
  templateUrl: './counter-app-signal.html',
  styleUrl: './counter-app-signal.css',
})
export class CounterAppSignal {

  counter:WritableSignal<number> = signal<number>(0);

  constructor() {
    effect(()=> {
      console.log("Counter Value",this.counter());
    })
  }

  increment(){
    this.counter.update((value)=> value + 1);
  }
  reset(){
    this.counter.set(0);
  }

  decrement(){
    if(this.counter() > 0){
      this.counter.update((value)=> value - 1);
    }
  }
}
