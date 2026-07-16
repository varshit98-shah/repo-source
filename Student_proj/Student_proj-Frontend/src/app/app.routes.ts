import { Routes } from '@angular/router';
import { LoginPage } from './features/auth/pages/login-page/login-page';
import { RegisterPage } from './features/auth/pages/register-page/register-page';
import { ForgotpPassword } from './features/auth/pages/forgotp-password/forgotp-password';
import { ResetPassword } from './features/auth/pages/reset-password/reset-password';
import { LayoutComponent } from './core/layout/layout.component';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  // Public Auth Routes
  { path: 'login', component: LoginPage },
  { path: 'register', component: RegisterPage },
  { path: 'forgot-password', component: ForgotpPassword },
  { path: 'reset-password', component: ResetPassword },
  
  // Protected Routes (Wrapped in Layout)
  {
    path: '',
    component: LayoutComponent,
    canActivate: [authGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { 
        path: 'dashboard', 
        loadChildren: () => import('./features/dashboard/dashboard.routes').then(m => m.dashboardRoutes) 
      },
      {
        path: 'students',
        loadChildren: () => import('./features/students/students.routes').then(m => m.studentsRoutes)
      },
      {
        path: 'profile',
        loadComponent: () => import('./features/profile/pages/profile/profile.component').then(m => m.ProfileComponent)
      }
    ]
  },
  
  // Fallback
  { path: '**', redirectTo: '/login' }
];
