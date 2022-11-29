import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-group-dialog',
  templateUrl: './group-dialog.component.html',
  styleUrls: ['./group-dialog.component.css']
})
export class GroupDialogComponent implements OnInit {

  form: FormGroup;
  loading = false;
  nameControl = new FormControl('', [Validators.required, this.nameValidator()]);


  constructor(
    public dialogRef: MatDialogRef<GroupDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public groups: string[],
    private http: HttpClient
  ) {
    this. form = new FormGroup({
      name: this.nameControl
    });
   }

  ngOnInit(): void {
  }

  save() {
    this.loading = true;
    this.http.post(`todo-group?name=${this.nameControl.value}`, null).subscribe({
      next: value => { this.dialogRef.close(value)},
      error: value => { this.dialogRef.close() }
    });
  }

  nameValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if(this.nameControl == undefined) return null;
      if(this.groups.find(x => x ==  this.nameControl.value?.toLowerCase().replace(' ', '')) != undefined){
        return { 'nameInvalid': true };
      }
      return null;
    };
  }

  close(){
    this.dialogRef.close();
  }

}
