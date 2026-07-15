import { Injectable, signal } from '@angular/core';

export interface ToastMessage {
  message: string;
  type: 'success' | 'error' | 'info';
}

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  toastSignal = signal<ToastMessage | null>(null);
  private timeout: any;

  show(message: string, type: 'success' | 'error' | 'info' = 'info') {
    // We use setTimeout to avoid the NG0100 ExpressionChangedAfterItHasBeenCheckedError 
    // when triggering a global toast from a child component.
    setTimeout(() => {
      this.toastSignal.set({ message, type });
      
      if (this.timeout) {
        clearTimeout(this.timeout);
      }
      
      // Auto-hide after 4 seconds
      this.timeout = setTimeout(() => {
        this.toastSignal.set(null);
      }, 4000);
    });
  }

  success(message: string) {
    this.show(message, 'success');
  }

  error(message: string) {
    this.show(message, 'error');
  }

  hide() {
    this.toastSignal.set(null);
  }
}
