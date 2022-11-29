import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { TodoGroup } from '../entities/todo-group';
import { GroupsComponent } from '../groups/groups.component';
import { TodoListComponent } from '../todo-list/todo-list.component';

@Component({
  selector: 'app-application',
  templateUrl: './application.component.html',
  styleUrls: ['./application.component.css']
})
export class ApplicationComponent implements OnInit, AfterViewInit {

  @ViewChild('todos') todosList: TodoListComponent;
  @ViewChild('groups') groups: GroupsComponent;

  constructor(private readonly http: HttpClient) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.http.get<TodoGroup[]>('todo-group').subscribe({
      next: res => {
        this.todosList.grid.load(res[0].todos);
        this.todosList.gridLoading = false;
        this.todosList.groupId = res[0].id;
        this.groups.default = res[0];
        this.groups.groups = res.filter(x => x.slug != 'todos')
      },  
      error: value => {this.error(value.message); this.todosList.gridLoading = false;}
    });
  }

  error(msg: string) {
    this.todosList.alertError(msg);
  }

}
