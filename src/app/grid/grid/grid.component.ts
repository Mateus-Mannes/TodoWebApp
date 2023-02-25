import { Component, Input, OnInit } from '@angular/core';
import { Todo } from 'src/app/entities/todo';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.css']
})
export class GridComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  @Input() todos: Todo[] = [new Todo(), new Todo()];

}
