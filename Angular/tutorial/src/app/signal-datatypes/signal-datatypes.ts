import { Component, effect, signal, WritableSignal } from '@angular/core';

@Component({
  selector: 'app-signal-datatypes',
  imports: [],
  templateUrl: './signal-datatypes.html',
  styleUrl: './signal-datatypes.css',
})
export class SignalDatatypes {
                    //in this section the datatyoe of the signal is defined 
                      //|
                      //|
                      //⌄
  Name:WritableSignal<string> = signal<string>("Nikhil");
                                      // ^
                                      // |
                                      // |
                                      // |
                                      // in this section the value datatype of the signal is defined

  //.set() is used when you dont want the signal input and want to change,Used when you want to assign a completely new value
  //.update() Used when the new value depends on the current value.

  constructor(){
    effect(()=> {
      console.log(this.Name());
    })
  }

isUpdated = false;

updateName() {

  if (this.isUpdated) {
    return; 
  }

  this.Name.update(value => value + " Barad");
  this.isUpdated = true;
}
}
