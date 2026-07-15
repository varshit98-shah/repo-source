import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardService, DashboardStats } from '../../services/dashboard.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  private dashboardService = inject(DashboardService);
  
  stats = signal<DashboardStats | null>(null);
  isLoading = signal(true);
  error = signal<string | null>(null);

  recentActivity = [
    { action: 'Admin updated Student X', time: '10 mins ago', type: 'update' },
    { action: 'User logged out', time: '1 hour ago', type: 'auth' },
    { action: 'Admin created Course Y', time: '2 hours ago', type: 'create' },
    { action: 'New Student registered', time: '5 hours ago', type: 'create' }
  ];

  ngOnInit() {
    this.loadStats();
  }

  loadStats() {
    this.isLoading.set(true);
    this.error.set(null);
    this.dashboardService.getStats().subscribe({
      next: (data) => {
        this.stats.set(data);
        this.isLoading.set(false);
      },
      error: (err) => {
        this.error.set(err.message || 'Failed to load stats');
        this.isLoading.set(false);
      }
    });
  }
}
