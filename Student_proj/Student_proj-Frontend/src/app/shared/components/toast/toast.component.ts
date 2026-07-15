import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastService } from '../../../core/services/toast.service';

@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="toast-container" *ngIf="toastService.toastSignal() as toast">
      <div class="toast-box" [ngClass]="toast.type">
        <span class="icon">
          <ng-container *ngIf="toast.type === 'success'">✓</ng-container>
          <ng-container *ngIf="toast.type === 'error'">✕</ng-container>
          <ng-container *ngIf="toast.type === 'info'">ℹ</ng-container>
        </span>
        <span class="message">{{ toast.message }}</span>
        <button class="close-btn" (click)="toastService.hide()">×</button>
      </div>
    </div>
  `,
  styleUrl: './toast.scss'
})
export class ToastComponent {
  toastService = inject(ToastService);
}
