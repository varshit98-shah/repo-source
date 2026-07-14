import { Component } from '@angular/core';

@Component({
  selector: 'app-inline-demo',
  imports: [],
  template: ` <input type="text" class ="box" placeholder="Enter Name"> `,
  styles: `.box{width: 200px;}`,
})
export class InlineDemo {}
