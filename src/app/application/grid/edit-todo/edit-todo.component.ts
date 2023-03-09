import { Component, ElementRef, Inject, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { Todo } from 'src/app/shared/entities/todo';

@Component({
  selector: 'app-edit-todo',
  templateUrl: './edit-todo.component.html',
  styleUrls: ['./edit-todo.component.css']
})
export class EditTodoComponent implements OnInit, AfterViewInit {

  form: FormGroup;
  @ViewChild('deadline') datePicker: ElementRef;

  constructor(private readonly _matDialogRef: MatDialogRef<EditTodoComponent>,
    @Inject(MAT_DIALOG_DATA) private readonly _data: Todo) {
      this. form = new FormGroup({
        description: new FormControl(_data.description, [Validators.required]),
        deadLine:  new FormControl(_data.deadLine) });
  }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    if(this.form.get('deadLine')?.value != null){
      let date = new Date(this.form.get('deadLine')?.value);
      this.datePicker.nativeElement.value = date.toISOString().split('T')[0];
    }
  }

  save() {
    this._matDialogRef.close(this._data)
  }

  close(){
    this._matDialogRef.close(undefined);
  }

}
