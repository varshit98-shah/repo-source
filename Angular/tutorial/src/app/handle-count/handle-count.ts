import { Component } from '@angular/core';
import { Common } from '../services/common';

@Component({
  selector: 'app-handle-count',
  imports: [],
  templateUrl: './handle-count.html',
  styleUrl: './handle-count.css',
})
export class HandleCount {

  constructor(public state:Common){}
}
