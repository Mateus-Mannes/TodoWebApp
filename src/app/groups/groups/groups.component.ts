import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AlertService } from 'src/app/alert-service';
import { Todo } from 'src/app/entities/todo';
import { TodoGroup } from 'src/app/entities/todo-group';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {

  constructor(private readonly _httpClient: HttpClient,private readonly _alertService : AlertService) { }

  ngOnInit(): void {
  }

  @Input() groups: TodoGroup[] = [{ id:0, name:'Todos', slug:'todos', todos:[new Todo()] },{ id:0, name:'g1', slug:'g1', todos:[new Todo()] }, { id:0, name:'g2', slug:'g2', todos:[new Todo(), new Todo()] }];
  todos : TodoGroup | undefined = this.groups.find(x => x.slug = 'todos');
  @Output() changeGroup : EventEmitter<TodoGroup>;
  loading = false;

  createGroup(groupName: string){
    this.loading  = true
    this._httpClient.post<TodoGroup>('todo-group', groupName).subscribe({
      next: res => {
        this.loading  = false;
        this.emitChangeGroup(res);
      },
      error: value => {
        this._alertService.alert('Error on adding list - '+value.message, 'danger');
        this.loading  = false;
      }
    });
  }

  deleteGroup(group: TodoGroup){
    this.removeList(group);
    this._httpClient.delete<TodoGroup>(`todo-group/${group.id}`).subscribe({
      next: res => {},
      error: value => {
        this._alertService.alert('Error on deleting list - '+value.message, 'danger');
        this.groups.push(group);
      }
    });
  }

  removeList(group: TodoGroup){
    let index = this.groups.indexOf(group);
    this.groups.splice(index, 1);
  }

  emitChangeGroup(group: TodoGroup){
    this.changeGroup.emit(group);
  }

}
