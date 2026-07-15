import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { StudentService } from '../../services/student.service';
import { StudentDTO } from '../../models/student.dto';

@Component({
  selector: 'app-student-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.scss']
})
export class StudentListComponent implements OnInit {
  private studentService = inject(StudentService);
  private router = inject(Router);

  students = signal<StudentDTO[]>([]);
  isLoading = signal(true);
  
  // RBAC signals
  canCreate = signal(false);
  canUpdate = signal(false);
  canDelete = signal(false);

  ngOnInit() {
    this.checkPermissions();
    this.loadStudents();
  }

  checkPermissions() {
    const permsStr = sessionStorage.getItem('user_permissions');
    if (permsStr) {
      try {
        const perms = JSON.parse(permsStr);
        // Look for 'student' menu permissions
        const studentPerms = perms.filter((p: any) => p.menuName.toLowerCase() === 'student');
        
        this.canCreate.set(studentPerms.some((p: any) => p.permission.toLowerCase() === 'create'));
        this.canUpdate.set(studentPerms.some((p: any) => p.permission.toLowerCase() === 'update'));
        this.canDelete.set(studentPerms.some((p: any) => p.permission.toLowerCase() === 'delete'));
      } catch (e) {
        console.error('Failed to parse permissions', e);
      }
    }
  }

  loadStudents() {
    this.isLoading.set(true);
    this.studentService.getAll().subscribe({
      next: (res) => {
        if (res && res.data) {
          this.students.set(res.data);
        }
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Error fetching students', err);
        this.isLoading.set(false);
      }
    });
  }

  deleteStudent(id: number) {
    if (!this.canDelete()) return;
    
    if (confirm('Are you sure you want to delete this student?')) {
      this.studentService.delete(id).subscribe({
        next: () => {
          this.students.update(list => list.filter(s => s.id !== id));
        },
        error: (err) => console.error('Failed to delete', err)
      });
    }
  }

  editStudent(id: number) {
    if (!this.canUpdate()) return;
    this.router.navigate(['/students/edit', id]);
  }
}
