import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { StudentService } from '../../services/student.service';
import { StudentDTO } from '../../models/student.dto';

import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-student-list',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.scss']
})
export class StudentListComponent implements OnInit {
  private studentService = inject(StudentService);
  private router = inject(Router);

  students = signal<StudentDTO[]>([]);
  isLoading = signal(true);
  
  // Pagination State
  pageNumber = signal(1);
  pageSize = signal(10);
  totalCount = signal(0);
  searchTerm = signal('');
  
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
    this.studentService.getPaginated(this.pageNumber(), this.pageSize(), this.searchTerm()).subscribe({
      next: (res) => {
        if (res && res.data) {
          this.students.set(res.data.items || []);
          this.totalCount.set(res.data.totalCount || 0);
        }
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Error fetching students', err);
        this.isLoading.set(false);
      }
    });
  }

  onSearchChange() {
    this.pageNumber.set(1);
    this.loadStudents();
  }

  onPageChange(newPage: number) {
    if (newPage >= 1 && newPage <= this.totalPages()) {
        this.pageNumber.set(newPage);
        this.loadStudents();
    }
  }

  onPageSizeChange(event: Event) {
    const size = parseInt((event.target as HTMLSelectElement).value, 10);
    this.pageSize.set(size);
    this.pageNumber.set(1);
    this.loadStudents();
  }

  totalPages(): number {
    return Math.ceil(this.totalCount() / this.pageSize());
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
