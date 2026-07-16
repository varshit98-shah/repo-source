import { Component, inject, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { AuthService } from '../../features/auth/services/auth.service';
import { decodeJwt } from '../utils/jwt.utils';

import { ThemeService } from '../services/theme.service';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit, OnDestroy {
  private authService = inject(AuthService);
  private router = inject(Router);
  public themeService = inject(ThemeService);

  isSidebarCollapsed = false;
  userName = 'User';
  userRole = 'User';

  // Bound reference so we can remove the listener on destroy
  private pageshowHandler = (event: PageTransitionEvent) => {
    // event.persisted = true means the browser restored this page from BFCache
    // Always clear session and redirect to login when coming back from outside the app
    if (event.persisted) {
      this.authService.logout();
      window.location.replace('/login');
    }
  };
  
  ngOnInit() {
    // Listen for BFCache restoration
    window.addEventListener('pageshow', this.pageshowHandler);

    const token = this.authService.getToken();
    if (!token) {
      window.location.replace('/login');
      return;
    }

    const decoded = decodeJwt(token);
    if (decoded) {
      this.userName = decoded.Name || decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] || 'User';
      this.userRole = decoded.PrimaryRole || decoded.role || decoded.Role || decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || 'User';
      
      if (Array.isArray(this.userRole)) {
          this.userRole = this.userRole[0];
      }
    }
  }

  ngOnDestroy() {
    window.removeEventListener('pageshow', this.pageshowHandler);
  }

  toggleSidebar() {
    this.isSidebarCollapsed = !this.isSidebarCollapsed;
  }

  logout() {
    this.authService.logout();
    window.location.replace('/login');
  }
}
