import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { api_config } from '../../../../config/api.config';
import { ToastService } from '../../../../core/services/toast.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  private fb = inject(FormBuilder);
  private http = inject(HttpClient);
  private toast = inject(ToastService);

  profileForm!: FormGroup;
  isLoading = signal(true);
  isSubmitting = signal(false);

  ngOnInit() {
    this.profileForm = this.fb.group({
      id: [null],
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      address: ['', Validators.required]
    });
    this.loadProfile();
  }

  loadProfile() {
    this.isLoading.set(true);
    this.http.get<any>(`${api_config.base_url}api/Profile`).subscribe({
      next: (res) => {
        if (res && res.data) {
          const p = res.data;
          const phoneVal = p.phone && p.phone.startsWith('+91') ? p.phone.substring(3) : p.phone;
          this.profileForm.patchValue({
            id: p.id,
            name: p.name,
            email: p.email,
            phone: phoneVal,
            address: p.address
          });
        }
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Failed to load profile', err);
        this.toast.error('Failed to load profile');
        this.isLoading.set(false);
      }
    });
  }

  onSubmit() {
    if (this.profileForm.invalid) {
      this.profileForm.markAllAsTouched();
      return;
    }

    this.isSubmitting.set(true);
    const formValue = { ...this.profileForm.value };

    // Trim string fields
    for (const key in formValue) {
      if (typeof formValue[key] === 'string') {
        formValue[key] = formValue[key].trim();
      }
    }

    if (!formValue.phone.startsWith('+91')) {
      formValue.phone = '+91' + formValue.phone;
    }

    this.http.put(`${api_config.base_url}api/Profile`, formValue).subscribe({
      next: () => {
        this.toast.success('Profile updated successfully');
        this.isSubmitting.set(false);
      },
      error: (err) => {
        console.error('Failed to update profile', err);
        this.toast.error(err.error?.message || 'Failed to update profile');
        this.isSubmitting.set(false);
      }
    });
  }
}
