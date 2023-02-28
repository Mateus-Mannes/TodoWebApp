import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { AlertService } from 'src/app/alert-service';
import { Todo } from 'src/app/entities/todo';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.css']
})
export class GridComponent implements OnInit {

  constructor(private readonly _httpClient: HttpClient,
    private readonly _alertService : AlertService) { }

  ngOnInit(): void {
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

    updateTodo(){
      
    }
  }
}
