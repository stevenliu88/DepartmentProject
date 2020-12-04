import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-show-dep',
  templateUrl: './show-dep.component.html',
  styleUrls: ['./show-dep.component.css']
})
export class ShowDepComponent implements OnInit {
  
  constructor(private sharedService: SharedService) { }

  DepartmentList: any = [];
  ModalTitle: string;
  ActivateAddEditDepComp: boolean = false;

  dep: any;

  ngOnInit() {
    this.refreshDepList();
  }
  addClick() {
    this.dep = {
      DepartmentId: 0,
      DepartmentName: ''
    };

    this.ModalTitle="Add Department";
    this.ActivateAddEditDepComp = true;
  }

  editClick(item: any) {
    this.dep = item;
    this.ModalTitle = "Edit Department";
    this.ActivateAddEditDepComp = true;
  }
  closeClick() {
    this.ActivateAddEditDepComp = false;
    this.refreshDepList();
  }
  deleteClick(item: any) {
    if (confirm('Are you sure to remove department')) {
      this.sharedService.deleteDepartment(item.DepartmentId).subscribe(res=> {
        this.refreshDepList();
        alert(res.toString());
      });
    }
  }
  refreshDepList() {
    this.sharedService.getDepList().subscribe(data => {
      this.DepartmentList = data;
    });
  }
}
