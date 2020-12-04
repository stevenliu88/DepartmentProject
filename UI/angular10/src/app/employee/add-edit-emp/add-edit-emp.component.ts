import { Component, Input, OnInit } from '@angular/core';
import { SharedService } from '../../shared.service';
@Component({
  selector: 'app-add-edit-emp',
  templateUrl: './add-edit-emp.component.html',
  styleUrls: ['./add-edit-emp.component.css']
})
export class AddEditEmpComponent implements OnInit {

  constructor(private sharedService: SharedService) { }
  @Input() editEmp: any;
  EmployeeId: string;
  EmployeeName: string;
  Department: string;
  DateOfJoining: string;
  PhotoFileName: string;
  PhotoFilePath: string;

  DepartmentList: any = [];
  ngOnInit() {
    this.loadDepartmentList();
  }

  loadDepartmentList() {
    this.sharedService.GetAllDepartmentNames().subscribe((data: any) => {
      console.log('data',data);
      this.DepartmentList = data;
      this.EmployeeId = this.editEmp.EmployeeId;
      this.EmployeeName = this.editEmp.EmployeeName;
      this.Department = this.editEmp.Department;
      this.DateOfJoining = this.editEmp.DateOfJoining;
      this.PhotoFileName = this.editEmp.PhotoFileName;
      this.PhotoFilePath = this.sharedService.PhotoUrl + this.PhotoFileName;
      console.log('PhotoFilePath', this.PhotoFilePath);
    });
  }
  addEmployee() {
    const employee = {
      EmployeeId : this.EmployeeId,
      EmployeeName : this.EmployeeName,
      Department : this.Department,
      DateOfJoining : this.DateOfJoining,
      PhotoFileName : this.PhotoFileName
    };

    this.sharedService.addEmployee(employee).subscribe(res => {
      alert(res.toString());
    });
  }

  updateEmployee() {
    const employee = {
      EmployeeId : this.EmployeeId,
      EmployeeName : this.EmployeeName,
      Department : this.Department,
      DateOfJoining : this.DateOfJoining,
      PhotoFileName : this.PhotoFileName
    };
    console.log('employee', employee);
    this.sharedService.updateEmployee(employee).subscribe(res => {
      alert(res.toString());
    });
  }

  uploadPhoto(event) {
    const file = event.target.files[0];
    const formData: FormData = new FormData();
    formData.append('uploadedFile', file, file.name);

    this.sharedService.uploadPhoto(formData).subscribe((data: any) => {
      console.log('upload file', data);
      this.PhotoFileName = data.toString();
      this.PhotoFilePath = this.sharedService.PhotoUrl + this.PhotoFileName;
    });
  }
}
