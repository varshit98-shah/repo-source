import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ToastService } from '../../../../core/services/toast.service';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './reset-password.html',
  styleUrl: './reset-password.scss',
})
export class ResetPassword implements OnInit {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private toastService = inject(ToastService);

  emailToReset = '';
  
  resetForm = this.fb.group({
    otp: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(6)]],
    newPassword: ['', [Validators.required, Validators.minLength(6)]]
  });

  isLoading = signal(false);
  showPassword = signal(false);

  togglePassword() {
    this.showPassword.update(v => !v);
  }

  ngOnInit() {
    // Grab the email passed from the forgot-password page
    this.route.queryParams.subscribe(params => {
      if (params['email']) {
        this.emailToReset = params['email'];
      }
    });
  }

  allowOnlyNumbers(event: KeyboardEvent) {
    const charCode = event.key.charCodeAt(0);
    // Allow only digits (0-9)
    if (charCode < 48 || charCode > 57) {
      event.preventDefault();
    }
  }

  onSubmit() {
    if (this.resetForm.invalid || !this.emailToReset) {
      this.resetForm.markAllAsTouched();
      if (!this.emailToReset) this.toastService.error('Email is missing. Please restart the forgot password process.');
      return;
    }

    this.isLoading.set(true);

    this.authService.resetPassword(this.emailToReset, this.resetForm.value).subscribe({
      next: () => {
        this.isLoading.set(false);
        this.toastService.success('Password reset successfully! Redirecting to login...');
        setTimeout(() => this.router.navigate(['/login']), 2000);
      },
      error: (err) => {
        this.isLoading.set(false);
        const msg = err.error?.message || 'Failed to reset password. Please check your OTP and try again.';
        this.toastService.error(msg);
      }
    });
  }
}
