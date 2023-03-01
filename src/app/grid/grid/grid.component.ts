import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AlertService } from 'src/app/alert-service';
import { EditComponent } from 'src/app/edit/edit.component';
import { Todo } from 'src/app/entities/todo';
import { EditTodoComponent } from '../edit-todo/edit-todo.component';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.css']
})
export class GridComponent implements OnInit {

  constructor(private readonly _httpClient: HttpClient,
    private readonly _alertService : AlertService,
    private readonly _matDialog: MatDialog) { }

  ngOnInit(): void {
    let dialogRef = this._matDialog.open(EditTodoComponent, { data: new Todo(), maxWidth: "700px", width: "90%" });
  }

  @Input() todos: Todo[] = [new Todo(), new Todo(),new Todo(), new Todo(),new Todo(), new Todo(),new Todo(), new Todo(),new Todo(), new Todo(),new Todo(), new Todo(),new Todo(), new Todo(),new Todo(), new Todo()];

  createTodo(todo: Todo) {
    this.todos.push(todo);
    this._httpClient.post<Todo>('todo', todo).subscribe({
      next: res => {},
      error: value => {
        this._alertService.alert('Error on adding todo - '+value.message, 'danger');
        this.removeTodoFromGrid(todo);
      }
    });
  }

  removeTodoFromGrid(todo: Todo){
    let index = this.todos.indexOf(todo);
    this.todos.splice(index, 1);
  }

  deleteTodo(todo: Todo){
    this.removeTodoFromGrid(todo);
    this._httpClient.delete(`todo/${todo.id}`).subscribe({
      next: () => {}, 
      error: value => {
        this._alertService.alert('Error on completing todo - '+value.message, 'danger');
        this.todos.push(todo);
      }
    });
  }

  updateTodo(){
    return;
  }
}
