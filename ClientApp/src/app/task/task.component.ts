import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Pagination } from '../_models/pagination';
import { User } from '../_models/user';
import { TaskService } from '../_services/task.service';

@Component({
  selector: 'app-home',
  templateUrl: './task.component.html',
})
export class TaskComponent {

  model: any = {};

  tasks: Task[] = [];
  pagination!: Pagination;
  container = 'Unread';
  pageNumber = 1;
  pageSize = 5;


  constructor(private http: HttpClient, private taskerService: TaskService) { }

  ngOnInit() { 
    this.loadTasks();
  }

  addTask() {
    var user =  localStorage.getItem('user');
    this.taskerService.addTask(12,this.model).subscribe(response => {
     console.log(response);
    });
  }

  loadTasks() {
      this.taskerService.getTasks(this.pageNumber, this.pageSize).subscribe(response => {
      this.tasks = response.result!;
      this.pagination = response.pagination;
      
    })
  }
}
