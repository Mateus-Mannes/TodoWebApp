import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { Todo } from 'src/app/entities/todo';

@Component({
  selector: 'app-edit-todo',
  templateUrl: './edit-todo.component.html',
  styleUrls: ['./edit-todo.component.css']
})
export class EditTodoComponent implements OnInit {

  form: FormGroup;

  constructor(private readonly _matDialogRef: MatDialogRef<EditTodoComponent>,
    @Inject(MAT_DIALOG_DATA) private readonly _data: Todo) {
      this. form = new FormGroup({
        description: new FormControl(_data.description, [Validators.required]),
        deadLine:  new FormControl(_data.deadLine) });
  }

  ngOnInit(): void {
  }

  save() {
    this._matDialogRef.close(this._data)
  }

  close(){
    this._matDialogRef.close(undefined);
  }

}
