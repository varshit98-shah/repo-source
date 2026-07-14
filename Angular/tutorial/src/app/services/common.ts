import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class Common {
  count = signal(0);

  decrement(){
    if(this.count()>0){
      this.count.update((c)=>c-1);
    }
  }
  increment(){
      this.count.update((c)=>c+1);
    
  }
  reset(){
    this.count.set(0);
  }
}
