import { Component } from '@angular/core';

@Component({
  selector: 'app-datatypes',
  imports: [],
  templateUrl: './datatypes.html',
  styleUrl: './datatypes.css',
})
export class Datatypes {
  name: number| string = 10;
  //name = 10; this also works fine when we use it then the developer knows that it accepts only strinig like that
  update(val:string,value:number){
    console.log(val);
    this.name = value;
  }

  understanding(){
    //if the datatpye is declared like property which it is now currently declare and the property name is name then it accepts only number
    //we can also assign multiple datatypes to a property or variable by using (|) pipe operator and datatype name
    //this.name = "Hello" then it is valid because we had declared second dataype also in the property name 
    this.name = "Hello"; // in this the name also changes and the final value will be hello
    console.log(this.name);
    
  }

  understanding2(event: PointerEvent| Event){
    //in event it can also be like this same as propeerty can also have multiple types and same using pipe operator
    console.log(event);
  }
}
