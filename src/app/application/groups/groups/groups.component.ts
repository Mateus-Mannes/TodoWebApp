import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertService } from 'src/app/shared/services/alert-service';
import { AuthService } from 'src/app/shared/services/auth-service';
import { TodoGroup } from 'src/app/shared/entities/todo-group';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FadeInOut } from 'src/app/shared/services/animation';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css'],
  animations: [FadeInOut(300, 200, true)]
})
export class GroupsComponent implements OnInit {

  form: FormGroup;
  nameControl = new FormControl('', [Validators.required]);
  @Input() groups: TodoGroup[] = [];
  todos : TodoGroup;
  selectedGroup : TodoGroup;
  loading = false;

  constructor(private readonly _httpClient: HttpClient,
    private readonly _alertService : AlertService,
    private readonly _authService : AuthService,
    private readonly _router : Router) {
      this. form = new FormGroup({
        name: this.nameControl
      });
     }

  ngOnInit(): void {
    this.todos = this.groups.filter(x => x.slug = 'todos')[0];
    this.selectedGroup = this.todos;
  }

  createGroup(){
    if(this.nameControl.value == '' || this.nameControl.value == undefined) return;
    this.loading  = true
    this._httpClient.post<TodoGroup>(`todo-group/${this.nameControl.value}`, null).subscribe({
      next: res => {
        this.groups.push(res);
        this.nameControl.setValue('');
        this.loading  = false;
        this.changeGroup(res);
      },
      error: value => {
        this._alertService.alert('Error on adding list - '+value.error, 'danger');
        this.closeNavBar();
        this.loading  = false;
      }
    });
  }

  deleteGroup(group: TodoGroup){
    this.removeList(group);
    this._httpClient.delete<TodoGroup>(`todo-group/${group.id}`).subscribe({
      next: res => {},
      error: value => {
        this._alertService.alert('Error on deleting list - '+value.error, 'danger');
        this.groups.push(group);
        this.closeNavBar();
      }
    });
  }

  removeList(group: TodoGroup){
    if(group.id == this.selectedGroup.id){
      this.selectedGroup = this.todos;
    }
    let index = this.groups.indexOf(group);
    this.groups.splice(index, 1);
  }

  changeGroup(group: TodoGroup){
    this.selectedGroup = group;
    this.closeNavBar();
  }

  logout(){
    this._authService.logout();
    this._router.navigateByUrl('/login');
  }

  closeNavBar(){
    document.getElementById('closeNavBar')?.click();
  }

}
