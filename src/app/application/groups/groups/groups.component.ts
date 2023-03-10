import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { AlertService } from 'src/app/shared/services/alert-service';
import { AuthService } from 'src/app/shared/services/auth-service';
import { Todo } from 'src/app/shared/entities/todo';
import { TodoGroup } from 'src/app/shared/entities/todo-group';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {

  form: FormGroup;
  nameControl = new FormControl('', [Validators.required]);
  @Input() groups: TodoGroup[] = [{ id:0, name:'Todos', slug:'todos', todos:[new Todo()] },{ id:0, name:'g1', slug:'g1', todos:[new Todo()] }, { id:0, name:'g2', slug:'g2', todos:[new Todo(), new Todo()] }];
  todos : TodoGroup | undefined = this.groups.find(x => x.slug = 'todos');
  @Output() changeGroup : EventEmitter<TodoGroup>;
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
  }

  createGroup(){
    if(this.nameControl.value == '' || this.nameControl.value == undefined) return;
    this.loading  = true
    this._httpClient.post<TodoGroup>('todo-group', {name: this.nameControl.value}).subscribe({
      next: res => {
        this.loading  = false;
        this.emitChangeGroup(res);
      },
      error: value => {
        this._alertService.alert('Error on adding list - '+value.message, 'danger');
        this.loading  = false;
      }
    });
  }

  deleteGroup(group: TodoGroup){
    this.removeList(group);
    this._httpClient.delete<TodoGroup>(`todo-group/${group.id}`).subscribe({
      next: res => {},
      error: value => {
        this._alertService.alert('Error on deleting list - '+value.message, 'danger');
        this.groups.push(group);
      }
    });
  }

  removeList(group: TodoGroup){
    let index = this.groups.indexOf(group);
    this.groups.splice(index, 1);
  }

  emitChangeGroup(group: TodoGroup | undefined){
    this.changeGroup.emit(group);
  }

  activeGroup(group: TodoGroup){
    let old = (document.querySelector('.active') as HTMLImageElement);
    old.className = 'nav-link';
    let newGp = (document.getElementById('gp'+group?.id) as HTMLImageElement);
    newGp.className = 'nav-link active';
  }

  logout(){
    this._authService.logout();
    this._router.navigateByUrl('/login');
  }

}
