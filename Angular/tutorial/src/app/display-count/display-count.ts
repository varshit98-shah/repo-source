import { Component } from '@angular/core';
import { Common } from '../services/common';

@Component({
  selector: 'app-display-count',
  imports: [],
  templateUrl: './display-count.html',
  styleUrl: './display-count.css',
})
export class DisplayCount {
  constructor(public state:Common){}

}
