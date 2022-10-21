import { HttpClient } from '@angular/common/http';
import { Component, Injectable, OnInit } from '@angular/core';
import { Todo } from './entities/todo';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

@Injectable()
export class AppComponent implements OnInit {
  title = 'todo-app';
  todos: Todo[];
  columnsToDisplay = ['description'];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get<Todo[]>('todo').subscribe(res => {
      this.todos = res;
    });
  }

  
}
