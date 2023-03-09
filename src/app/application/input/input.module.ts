import { InputComponent } from './input/input.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
  declarations: [InputComponent],
  imports: [
    CommonModule
  ], exports: [ InputComponent ]
})
export class InputModule { }
