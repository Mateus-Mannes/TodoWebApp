import { GroupDialogComponent } from './../group-dialog/group-dialog.component';
import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AuthService } from '../auth-service';
import { TodoGroup } from '../entities/todo-group';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {
  default: TodoGroup;
  groups: TodoGroup[] = [];

  @Output() error: EventEmitter<string> = new EventEmitter<string>();

  constructor(private readonly authService: AuthService,
    private readonly router: Router,
    private readonly http: HttpClient,
    public dialog: MatDialog) { }

  ngOnInit(): void {

  }

  logout(){
    this.authService.logout();
    this.router.navigateByUrl('/login');
  }

  addGroup(){
    if(this.groups.length >= 4){
      this.error.emit('Limit of 5 lists reached');
      return;
    }
    let groupsNames = this.groups.map(x => x.name);
    groupsNames.push(this.default.name);
    let slugs = groupsNames.map(x => x.toLowerCase().replace(' ', ''));
    let dialog = this.dialog.open(GroupDialogComponent, { data: slugs, width: '400px' })
    dialog.afterClosed().subscribe({
      next: value => {
        if(value != undefined){
          this.groups.push(value);
        }
      }
    })
  }

}
