import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Todo } from 'src/app/entities/todo';
import { TodoGroup } from 'src/app/entities/todo-group';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  @Input() groups: TodoGroup[] = [{ id:0, name:'Todos', slug:'todos', todos:[new Todo()] },{ id:0, name:'g1', slug:'g1', todos:[new Todo()] }, { id:0, name:'g2', slug:'g2', todos:[new Todo(), new Todo()] }];
  todos : TodoGroup | undefined = this.groups.find(x => x.slug = 'todos');
  @Output() changeGroup : EventEmitter<TodoGroup>;

}
