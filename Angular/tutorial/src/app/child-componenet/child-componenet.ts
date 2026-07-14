import { Component, EventEmitter, Input, output, Output } from '@angular/core';

@Component({
  selector: 'app-child-componenet',
  imports: [],
  templateUrl: './child-componenet.html',
  styleUrl: './child-componenet.css',
})
export class ChildComponenet {
  @Input() name:string|undefined
  @Output() getstudentname = new EventEmitter()

  @Output()deletenames = new EventEmitter()

  getname(name:string|undefined){
    this.getstudentname.emit(name);

  }

  deletename(name:string|undefined){

    this.deletenames.emit(name);
    }

}
