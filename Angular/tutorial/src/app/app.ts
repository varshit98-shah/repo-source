import { Component, signal } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { EventsBinding } from './events-binding/events-binding';
import { Datatypes } from './datatypes/datatypes';
import { PropertyBinding } from './property-binding/property-binding';
import { SignalsP } from './signals-p/signals-p';
import { SignalEffect } from './signal-effect/signal-effect';
import { SignalDatatypes } from './signal-datatypes/signal-datatypes';
import { CounterAppSignal } from './counter-app-signal/counter-app-signal';
import { GetSetSignal } from './get-set-signal/get-set-signal';
import { ControlStatement } from './control-statement/control-statement';
import { TwoWayBinding } from './two-way-binding/two-way-binding';
import { ToDoList } from './to-do-list/to-do-list';
import { InlineDemo } from './inline-demo/inline-demo';
import { ChildComponenet } from './child-componenet/child-componenet';
import { DisplayCount } from './display-count/display-count';
import { HandleCount } from './handle-count/handle-count';
import { PipesExample } from './pipes-example/pipes-example';
import { CustomPipeImplementation } from './custom-pipe-implementation/custom-pipe-implementation';
import { HeaderComponent } from './header-component/header-component';
import { ContainerExampleUser } from './pages/container-example-user/container-example-user';
import { ReactiveForm } from './reactive-form/reactive-form';
import { SignalForm } from './signal-form/signal-form';
import { TemplateDrivenForm } from './template-driven-form/template-driven-form';
import { Login } from './auth/pages/login/login';


@Component({
  selector: 'app-root',
  imports: [EventsBinding,Datatypes,PropertyBinding,SignalsP,SignalEffect,SignalDatatypes,CounterAppSignal,GetSetSignal,ControlStatement,TwoWayBinding,ToDoList,InlineDemo,ChildComponenet,DisplayCount,HandleCount,PipesExample,CustomPipeImplementation,RouterOutlet,HeaderComponent,ContainerExampleUser,
    ReactiveForm,SignalForm,TemplateDrivenForm,Login],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('tutorial');

  getName(a:number, b:number): string
  {
    return (a+b).toString();
  }

  user = signal("Nikhil Barad");

  userarray = signal(["Nikhil","Sam","Patrick","Henil"])
  newuser = signal("");
  selectname = signal("");

  AddUser(){
    if(this.newuser()){
    this.userarray.update((user)=>([...user,this.newuser()]));
    this.newuser.set("");
    }

  }

  // removeuser(name:string){
  //   this.userarray.update((user)=> user.filter((item)=> item!=name))
  // }
  deletename(name:string){
    console.log(name);
    this.userarray.update((user)=> user.filter((item)=> item!=name))
  }

  selecteduser(name:string){
    console.log(name);
    this.selectname.set(name);
  }
}
