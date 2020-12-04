import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class SharedService {
  readonly APIurl = 'https://localhost:5001/api';
  readonly PhotoUrl = 'https://localhost:5001/Photos/';
  readonly baseDepartmentUrl = this.APIurl + `/Department`;
  readonly baseEmployeeUrl = this.APIurl + `/Employee`;
  constructor(private http: HttpClient) { }

  getDepList(): Observable<any[]> {
    return this.http.get<any[]>(this.baseDepartmentUrl);
  }

  addDepartment(dep: any): Observable<any>{
    return this.http.post<any>(this.baseDepartmentUrl, dep);
  }

  updateDepartment(dep: any): Observable<any> {
    return this.http.put<any>(this.baseDepartmentUrl, dep);
  }
  deleteDepartment(depId: any): Observable<any>{
    return this.http.delete<any>(this.baseDepartmentUrl + '/' + depId);
  }

  getEmployeeList(): Observable<any[]>{
    return this.http.get<any[]>(this.baseEmployeeUrl);
  }

  addEmployee(emp: any): Observable<any> {
    return this.http.post<any>(this.baseEmployeeUrl, emp);
  }

  updateEmployee(emp: any): Observable<any> {
    return this.http.put<any>(this.baseEmployeeUrl, emp);
  }
  deleteEmployee(empId: any): Observable<any> {
    return this.http.delete(this.baseEmployeeUrl + '/' + empId);
  }

  uploadPhoto(val: any): Observable<any> {
    return this.http.post<any>(this.baseEmployeeUrl + '/SaveFile', val);
  }

  GetAllDepartmentNames(): Observable<any[]> {
    return this.http.get<any[]>(this.baseEmployeeUrl + '/GetAllDepartmentNames');
  }
}
