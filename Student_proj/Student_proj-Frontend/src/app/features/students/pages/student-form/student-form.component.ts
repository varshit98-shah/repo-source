import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { StudentService } from '../../services/student.service';

@Component({
  selector: 'app-student-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './student-form.component.html',
  styleUrls: ['./student-form.component.scss']
})
export class StudentFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private studentService = inject(StudentService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  studentForm!: FormGroup;
  isEditMode = false;
  studentId: number | null = null;
  isLoading = signal(false);
  isSubmitting = signal(false);
  
  // To match the Auth UI style for password
  showPassword = signal(false);

  ngOnInit() {
    this.initForm();
    
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.isEditMode = true;
        this.studentId = +id;
        this.loadStudent(this.studentId);
      }
    });
  }

  initForm() {
    this.studentForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      address: ['', Validators.required],
      password: [''] // Conditionally required
    });

    if (!this.isEditMode) {
      this.studentForm.get('password')?.setValidators([Validators.required, Validators.minLength(6)]);
    }
    this.studentForm.get('password')?.updateValueAndValidity();
  }

  loadStudent(id: number) {
    this.isLoading.set(true);
    this.studentService.getById(id).subscribe({
      next: (res) => {
        if (res && res.data) {
          const s = res.data;
          this.studentForm.patchValue({
            name: s.name,
            email: s.email,
            phone: s.phone,
            address: s.address
          });
        }
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Failed to load student', err);
        this.isLoading.set(false);
      }
    });
  }

  togglePassword() {
    this.showPassword.update(v => !v);
  }

  onSubmit() {
    if (this.studentForm.invalid) {
      this.studentForm.markAllAsTouched();
      return;
    }

    this.isSubmitting.set(true);
    const formValue = { ...this.studentForm.value };
    
    // Trim string fields to prevent backend validation errors (400 Bad Request)
    for (const key in formValue) {
      if (typeof formValue[key] === 'string') {
        formValue[key] = formValue[key].trim();
      }
    }

    if (this.isEditMode && this.studentId) {
      // Remove password from payload if it's empty (maybe backend ignores it, or we just don't send it)
      this.studentService.update(this.studentId, formValue).subscribe({
        next: () => {
          this.isSubmitting.set(false);
          this.router.navigate(['/students']);
        },
        error: (err) => {
          console.error('Update failed', err);
          this.isSubmitting.set(false);
        }
      });
    } else {
      this.studentService.create(formValue).subscribe({
        next: () => {
          this.isSubmitting.set(false);
          this.router.navigate(['/students']);
        },
        error: (err) => {
          console.error('Create failed', err);
          this.isSubmitting.set(false);
        }
      });
    }
  }
}
