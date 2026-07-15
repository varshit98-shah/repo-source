import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { api_config } from '../../../config/api.config';
import { StudentDTO } from '../models/student.dto';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  private http = inject(HttpClient);

  getAll(): Observable<any> {
    return this.http.get(`${api_config.base_url}${api_config.endpoints.students.getAll}`);
  }

  getById(id: number): Observable<any> {
    return this.http.get(`${api_config.base_url}${api_config.endpoints.students.getById(id)}`);
  }

  create(student: any): Observable<any> {
    // The backend CreateStudent uses RegisterDTO which requires Password
    return this.http.post(`${api_config.base_url}${api_config.endpoints.students.create}`, student);
  }

  update(id: number, student: any): Observable<any> {
    return this.http.put(`${api_config.base_url}${api_config.endpoints.students.update(id)}`, student);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${api_config.base_url}${api_config.endpoints.students.delete(id)}`);
  }
}
