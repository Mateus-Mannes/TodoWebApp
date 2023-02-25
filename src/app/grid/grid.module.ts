import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GridComponent } from './grid/grid.component';
import { TodoComponent } from './todo/todo.component';
import {MatIconModule} from '@angular/material/icon';


@NgModule({
  declarations: [
    GridComponent,
    TodoComponent
  ],
  imports: [
    CommonModule,
    MatIconModule
  ]
})
export class GridModule { }
