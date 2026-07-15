import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { AuthService } from '../../features/auth/services/auth.service';
import { decodeJwt } from '../utils/jwt.utils';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {
  private authService = inject(AuthService);
  private router = inject(Router);

  isSidebarCollapsed = false;
  userName = 'User';
  userRole = 'User';
  
  ngOnInit() {
    const token = this.authService.getToken();
    if (!token) {
      this.router.navigate(['/login']);
      return;
    }

    const decoded = decodeJwt(token);
    if (decoded) {
      this.userName = decoded.Name || decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] || 'User';
      this.userRole = decoded.role || decoded.Role || decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || 'User';
      
      if (Array.isArray(this.userRole)) {
          this.userRole = this.userRole[0];
      }
    }
  }

  toggleSidebar() {
    this.isSidebarCollapsed = !this.isSidebarCollapsed;
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
