import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GridComponent } from './grid/grid.component';
import { TodoComponent } from './todo/todo.component';
import {MatIconModule} from '@angular/material/icon';
import { EditTodoComponent } from './edit-todo/edit-todo.component';
import { MatDialogModule } from '@angular/material/dialog';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    GridComponent,
    TodoComponent,
    EditTodoComponent
  ],
  imports: [
    CommonModule,
    MatIconModule,
    MatDialogModule,
    ReactiveFormsModule
  ],
  exports: [ GridComponent ]
})
export class GridModule { }
