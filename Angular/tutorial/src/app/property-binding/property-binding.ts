import { Component } from '@angular/core';

@Component({
  selector: 'app-property-binding',
  imports: [],
  templateUrl: './property-binding.html',
  styleUrl: './property-binding.css',
})
export class PropertyBinding {
  isdisabled = false;
  inputreadonly = false;

  toggle(){
    this.isdisabled = !this.isdisabled
    this.inputreadonly = !this.inputreadonly
  }
}
