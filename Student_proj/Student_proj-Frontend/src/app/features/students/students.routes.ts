import { Routes } from '@angular/router';
import { StudentListComponent } from './pages/student-list/student-list.component';
import { StudentFormComponent } from './pages/student-form/student-form.component';

export const studentsRoutes: Routes = [
  { path: '', component: StudentListComponent },
  { path: 'new', component: StudentFormComponent },
  { path: 'edit/:id', component: StudentFormComponent }
];
