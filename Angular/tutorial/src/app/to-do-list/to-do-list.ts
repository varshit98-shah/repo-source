import { Component,signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-to-do-list',
  imports: [FormsModule],
  templateUrl: './to-do-list.html',
  styleUrl: './to-do-list.css',
})
export class ToDoList {

  title = signal("");
  task = signal([
    { id: 1, Title: "Title Details"} 
  ])

  AddTask(){
    if(this.title()){
      this.task.update((task)=>([...task,{id:task.length+1,Title:this.title()}]))
      this.title.set("");
    }
  }

  removetask(id:number){
    this.task.update((task)=> task.filter((task)=> task.id!=id));
  }


}
