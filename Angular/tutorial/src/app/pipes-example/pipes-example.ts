import { CommonModule } from '@angular/common';
import { Component,signal } from '@angular/core';

@Component({
  selector: 'app-pipes-example',
  imports: [CommonModule],
  templateUrl: './pipes-example.html',
  styleUrl: './pipes-example.css',
})
export class PipesExample {
  name = signal("Nikhil Barad")
  namme1 = signal("")

  date = signal(new Date());
  
  price = 10000;

  names=signal({
    name:"Nikhil Barad",
    age:22,
    email:"nikhil@test.com"
  })

  students = signal([{
    id: 1,
    name: 'nikhil barad',
    email: 'nikhil@gmail.com',
    courseFee: 25000,
    joiningDate: new Date('2025-07-10'),
    percentage: 0.85
  },
  {
    id: 2,
    name: 'john smith',
    email: 'john@gmail.com',
    courseFee: 30000,
    joiningDate: new Date('2025-06-15'),
    percentage: 0.92
  }])
  
  
}
