import { Component, signal, WritableSignal } from '@angular/core';

@Component({
  selector: 'app-get-set-signal',
  imports: [],
  templateUrl: './get-set-signal.html',
  styleUrl: './get-set-signal.css',
})
export class GetSetSignal {
  defaultvalue:WritableSignal<string> = signal('');

  defaultvalue2(){
    this.defaultvalue.set("Nikhil Barad");

  }

  setvalue(val:string){
    this.defaultvalue.set(val);
  }
}
