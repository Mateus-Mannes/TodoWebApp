import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, ElementRef, Injectable, OnInit, ViewChild } from '@angular/core';
import { MatDatepicker, MatDatepickerInput } from '@angular/material/datepicker';
import { Todo } from './entities/todo';
import { GridComponent } from './grid/grid.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit, AfterViewInit {
  title = 'todo-app';
  @ViewChild('grid') grid: GridComponent;
  @ViewChild('input') input: ElementRef;
  @ViewChild('datepicker') datepicker: MatDatepicker<Date>;
  pickedDate: Date | null = null;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    
  }

  ngAfterViewInit(): void {
    this.http.get<Todo[]>('todo').subscribe(res => {
      this.grid.load(res);
    });
  }

  addTodo() {
    let newTodo = {description: this.input.nativeElement.value,
                  deadLine: this.pickedDate, todoGroupId: 1, id: 0, userId: 1, createdAt: new Date()}
    this.http.post<Todo>('todo', newTodo).subscribe(res => {
      this.grid.load([res]);
      this.input.nativeElement.value = '';
    });
  }

  pickDate(){
    this.pickedDate =this.datepicker.datepickerInput.getStartValue();
  }
}
