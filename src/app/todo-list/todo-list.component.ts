import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, ElementRef, Injectable, OnInit, ViewChild } from '@angular/core';
import { MatDatepicker, MatDatepickerInput } from '@angular/material/datepicker';
import { Router } from '@angular/router';
import { AuthService } from '../auth-service';
import { Todo } from '../entities/todo';
import { TodoGroup } from '../entities/todo-group';
import { GridComponent } from '../grid/grid.component';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css']
})
export class TodoListComponent implements OnInit, AfterViewInit {
  title = 'todo-app';
  @ViewChild('grid') grid: GridComponent;
  @ViewChild('input') input: ElementRef;
  @ViewChild('datepicker') datepicker: MatDatepicker<Date>;
  pickedDate: Date | null = null;
  gridLoading = true;
  addingTodo = false;
  error = false;
  errorMsg = '';
  groupId: number;

  constructor(private http: HttpClient, private readonly authService: AuthService,
    private readonly router: Router) {}

  ngOnInit() {
    
  }

  ngAfterViewInit(): void {
    this.http.get<TodoGroup[]>('todo-group').subscribe({
      next: res => {
        this.grid.load(res[0].todos); 
        this.gridLoading = false;
        this.groupId = res[0].id;
      },
      error: value => {this.alertError(value.message); this.gridLoading = false;}
    });
  }

  addTodo() {
    if(this.input.nativeElement.value == '') return;
    this.addingTodo = true;
    let newTodo = {description: this.input.nativeElement.value,
                  deadLine: this.pickedDate, todoGroupId: this.groupId, id: 0, userId: 1, createdAt: new Date()}
    this.http.post<Todo>('todo', newTodo).subscribe({
      next: res => {this.grid.load([res]);
      this.input.nativeElement.value = '';
      this.pickedDate = null;
      this.addingTodo = false;}, 
      error: value => {this.alertError(value.message);this.addingTodo = false;}
    });
  }

  pickDate(){
    this.pickedDate =this.datepicker.datepickerInput.getStartValue();
  }

  alertError(msg: string){
    this.errorMsg = msg;
    this.error = true;
    this.delay(10000).then(()=> this.error = false);
  }

  delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
  } 
}
