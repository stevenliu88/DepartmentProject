import { Component, Input, OnInit } from '@angular/core';
import { SharedService } from '../../shared.service';
@Component({
  selector: 'app-add-edit-dep',
  templateUrl: './add-edit-dep.component.html',
  styleUrls: ['./add-edit-dep.component.css']
})
export class AddEditDepComponent implements OnInit {

  constructor(private sharedService: SharedService) { }
  @Input() editDep: any;

  DepartmentId: string;
  DepartmentName: string;
  ngOnInit() {
    this.DepartmentId = this.editDep.DepartmentId;
    this.DepartmentName = this.editDep.DepartmentName;
  }

  addDepartment() {
    const val = {
      DepartmentId: this.DepartmentId,
      DepartmentName: this.DepartmentName
    };
    this.sharedService.addDepartment(val).subscribe(res => {
        alert(res.toString());
    });
  }

  updateDepartment() {
    const dep = {
      DepartmentId: this.DepartmentId,
      DepartmentName: this.DepartmentName
    };
    this.sharedService.updateDepartment(dep).subscribe(res => {
      alert(res.toString());
    });
  }

}
