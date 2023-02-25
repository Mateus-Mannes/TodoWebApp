import { Component, EventEmitter, OnInit, Output, Input } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { Todo } from 'src/app/entities/todo';

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.css']
})
export class TodoComponent implements OnInit {

  @Output() completeTodo : EventEmitter<Todo> = new EventEmitter<Todo>();
  @Output() editTodo : EventEmitter<Todo> = new EventEmitter<Todo>();

  @Input() todo : Todo;

  constructor() { }

  ngOnInit(): void {
  }

  emitTodoComplete(){
    this.completeTodo.emit(this.todo);
  }

  emitTodoEdit(){
    this.editTodo.emit(this.todo);
  }

}
