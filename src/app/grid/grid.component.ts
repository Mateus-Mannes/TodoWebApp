import { Component, ElementRef, EventEmitter, Injectable, Input, OnInit, Output, ViewChild } from '@angular/core';
import { Todo } from '../entities/todo';
import { SelectionModel } from '@angular/cdk/collections';
import { MatTable } from '@angular/material/table';
import { HttpClient } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { EditComponent } from '../edit/edit.component';


@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.css']
})

@Injectable()
export class GridComponent implements OnInit {
  title = 'todo-app';
  todos: Todo[] = [];
  columnsToDisplay = ['description', 'deadLine', 'actions'];
  selection = new SelectionModel<Todo>(true, []);
  @ViewChild(MatTable) table: MatTable<Todo>;
  @Output('onError') onError: EventEmitter<any> = new EventEmitter();

  constructor(private http: HttpClient,
    public dialog: MatDialog) { }

  ngOnInit(): void {
    
  }

  load(todos: Todo[]){
    this.todos.push(... todos);
    this.table.renderRows();
  }

  delete(todo: Todo){
    let actions = (document.getElementById(todo.id.toString()) as HTMLImageElement);
    let defaultActionsHtml = actions.innerHTML;
    actions.innerHTML = '<div class="spinner-border spinner-border-sm" role="status"></div>'
    this.http.delete(`todo/${todo.id}`).subscribe({
      next: () => {
        this.todos = this.todos.filter(x => x.id != todo.id);
        this.table.renderRows();
      }, error: value => {
        actions.innerHTML = defaultActionsHtml;
        this.onError.emit(value.message);
      }
    });
  }

  edit(todo: Todo) {
    let dialogRef = this.dialog.open(EditComponent, { data: todo, width: "100%" });
    dialogRef.afterClosed().subscribe(todoUpdate => {
      if('description' in todoUpdate && 'id' in todoUpdate){
        let index = this.todos.findIndex(x => x.id == todo.id);
        this.todos[index].description = todoUpdate?.description;
        this.todos[index].deadLine = todoUpdate?.deadLine;
      } else if(todoUpdate != undefined) {
        this.onError.emit(todoUpdate.message);
      }
    })
  }

   /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.todos.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  toggleAllRows() {
    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }
    this.selection.select(...this.todos);
  }
}
