import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { api_config } from '../../../config/api.config';

export interface DashboardStats {
  totalStudents: number;
  activeCourses: number;
  totalSubjects: number;
  recentLogins: number;
  recentActivities?: any[];
}

export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T;
  statusCodes: number;
}

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private http = inject(HttpClient);
  private baseUrl = api_config.base_url;

  getStats(): Observable<DashboardStats> {
    return this.http.get<ApiResponse<DashboardStats>>(`${this.baseUrl}${api_config.endpoints.dashboard.getStats}`)
      .pipe(
        map(response => {
          if (response.success) {
            return response.data;
          }
          throw new Error(response.message);
        }),
        catchError(error => {
          console.error('Error fetching dashboard stats:', error);
          return throwError(() => error);
        })
      );
  }
}
