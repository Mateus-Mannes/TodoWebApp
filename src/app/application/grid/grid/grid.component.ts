import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AlertService } from 'src/app/shared/services/alert-service';
import { Todo } from 'src/app/shared/entities/todo';
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
  }

  @Input() todos: Todo[] = [{description: 'teste', id: 0, deadLine: new Date(), todoGroupId: 0, createdAt: new Date(), userId: 0}];

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

  updateTodo(todo: Todo){
    let dialogRef = this._matDialog.open(EditTodoComponent, { data: todo, maxWidth: "700px", width: "90%" });
    dialogRef.afterClosed().subscribe(todoUpdate => {
      if('description' in todoUpdate && 'id' in todoUpdate){
        let index = this.todos.findIndex(x => x.id == todo.id);
        this.todos[index].description = todoUpdate?.description;
        this.todos[index].deadLine = todoUpdate?.deadLine;

        this._httpClient.put(`todo`, todoUpdate).subscribe({
          next: () => {},
          error: value => {
            this._alertService.alert('Error on updating todo - '+ value.message, 'danger');
            this.todos[index].description = todo.description;
            this.todos[index].deadLine = todo.deadLine;
          }
        });

      } else if(todoUpdate != undefined) {
        this._alertService.alert('Error on updating todo', 'danger');
      }
    })
  }
}
