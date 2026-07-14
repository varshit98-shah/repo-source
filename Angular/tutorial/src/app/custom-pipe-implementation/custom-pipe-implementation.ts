import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { TrimTextPipe } from '../custom-pipes/trim-text-pipe';
import { CustomCurrencyPipe } from '../custom-pipes/custom-currency-pipe';

@Component({
  selector: 'app-custom-pipe-implementation',
  imports: [CommonModule,TrimTextPipe,CustomCurrencyPipe],
  templateUrl: './custom-pipe-implementation.html',
  styleUrl: './custom-pipe-implementation.css',
})
export class CustomPipeImplementation {
  name = signal("Nikhil Barad");


  price = signal(100);
}
