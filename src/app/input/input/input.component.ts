import { Component, OnInit, Output, ViewChild, EventEmitter, ElementRef } from '@angular/core';
import { MatDatepicker } from '@angular/material/datepicker';
import { MatInput } from '@angular/material/input';
import { raceWith } from 'rxjs';
import { Todo } from '../../entities/todo';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.css']
})
export class InputComponent implements OnInit {

  @ViewChild('picker') datepicker: MatDatepicker<Date | null>;
  @ViewChild('todoDescription') todoDescription: ElementRef;
  @Output() createTodo : EventEmitter<Todo> = new EventEmitter<Todo>();

  constructor() { }

  ngOnInit(): void {
  }

  discardPickedDate(){
    this.datepicker.select(null);
    this.datepicker.close();
  }

  emitTodoCreation(){
    const todo = new Todo();
    todo.description = this.todoDescription.nativeElement.value;
    todo.deadLine = this.datepicker.datepickerInput.getStartValue();
    if(todo.description != ''){
      this.createTodo.emit(todo);
      this.resetInput();
    }
  }

  resetInput(){
    this.todoDescription.nativeElement.value = '';
      this.discardPickedDate();
      setTimeout(()=>{ // this will make the execution after the above boolean has changed
        this.todoDescription.nativeElement.focus();
      },0);
  }

}
