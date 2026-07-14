import { Component, signal, WritableSignal } from '@angular/core';

@Component({
  selector: 'app-control-statement',
  imports: [],
  templateUrl: './control-statement.html',
  styleUrl: './control-statement.css',
})
export class ControlStatement {

  islogin = signal(false);
  showbtn = signal(true);

  status = signal("not started");

  users = signal(["Nikhil","John","Ben"]);

  usersdetail = signal([
    {id:1 ,Name:"Nikhil",Email:"nikhil@test.com"},
    {id:2, Name:"John",Email:"john@test.com"},
    {id:3, Name:"Ben",Email:"ben@test.com"}])

    days = signal("Sunday");



  handlelogin(val:boolean){
    this.islogin.set(val);
  }

  handleOption(event:Event){
     let target = event.target as HTMLInputElement;
     this.status.set(target.value);
  }

  change(event:Event){
    let target = event.target as HTMLSelectElement;
    this.days.set(target.value);
  }


}
