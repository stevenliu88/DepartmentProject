import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../shared.service';
@Component({
  selector: 'app-show-emp',
  templateUrl: './show-emp.component.html',
  styleUrls: ['./show-emp.component.css']
})
export class ShowEmpComponent implements OnInit {

  EmployeeList: any = [];
  ModalTitle: string;
  ActivateAddEditEmpComp: boolean = false;
  emp: any;
  constructor(private sharedService: SharedService) { }

  ngOnInit() {
    this.refreshEmployee();
  }

  addClick() {
    this.ModalTitle = "Add Employee";
    this.ActivateAddEditEmpComp = true;
    this.emp = {
       EmployeeId: 0,
       EmployeeName: '',
       Department: '',
       DateOfJoining: '',
       PhotoFileName: '113.jpg'
    };
  }

  closeClick() {
    this.ActivateAddEditEmpComp = false;
    this.refreshEmployee();
  }

  refreshEmployee() {
    this.sharedService.getEmployeeList().subscribe(res => {
        this.EmployeeList = res;
        console.log('res', res);
    });
  }

  editClick(EmpItem: any) {
    this.ModalTitle = 'Edit Employee';
    this.emp = EmpItem;
    console.log('EmpItem', EmpItem);
    this.ActivateAddEditEmpComp = true;
  }

  deleteClick(EmpItem: any) {
    if (confirm('Are you sure to remove employee')) {
      this.sharedService.deleteEmployee(EmpItem.EmployeeId).subscribe(res => {
        alert('Employee has been removed');
        this.refreshEmployee();
      });
    }
  }
}
