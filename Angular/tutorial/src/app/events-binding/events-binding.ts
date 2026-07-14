import { Component } from '@angular/core';

@Component({
  selector: 'app-events-binding',
  imports: [],
  templateUrl: './events-binding.html',
  styleUrl: './events-binding.css',
})
export class EventsBinding {
  count = 0;
  name = 'nikhil';
  counter(action: string)
  {
    if(action =='minus')
      {
        if(this.count > 0){
        this.count--
        }
      }else{
        this.count++
      } 
      //console.log(this.count)
      // this.callName() suppouse to call the function declared below in this same function when called
  }

  callName()
  {
    alert(this.name)
  }

  MouseHower()
  {
    console.log("Mouse Hovered")
  }

  // data:string = "";
  // update(){
  //   this.data = "nikhil"
  // }

  // KeyboardEvent(event: KeyboardEvent)
  // {
  //   console.log("The User Pressed: ${event.}")
  // }


}
