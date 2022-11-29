import { GroupDialogComponent } from './../group-dialog/group-dialog.component';
import { HttpClient } from '@angular/common/http';
import { AfterContentInit, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
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
  selected: number;

  @Output() error: EventEmitter<string> = new EventEmitter<string>();
  @Output() selectList: EventEmitter<number> = new EventEmitter<number>();

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
          value.todos = [];
          this.groups.push(value);
          this.selectGroupById(value.id);
        }
      }
    })
  }

  deleteGroup(id: number){
    let deleteButton = (document.getElementById(id.toString()) as HTMLImageElement);
    let deleteButtonHtml = deleteButton.innerHTML;
    deleteButton.innerHTML = '<div class="spinner-border spinner-border-sm" role="status"></div>'
    this.http.delete(`todo-group/${id}`).subscribe({
      next: () => {
        this.groups = this.groups.filter(x => x.id != id);
        if(id == this.selected) this.selectGroupById(this.default.id);
      }, error: value => {
        deleteButton.innerHTML = deleteButtonHtml;
        this.error.emit("Error on deleting list");
      }
    });
  }

  selectGroup(event: any, id: number){
    if(event.target.className.startsWith('mat-icon')) return;
    this.selectGroupById(id);    
  }

  selectGroupById(id: number){
    let html = (document.getElementById(this.selected.toString()) as HTMLImageElement);
    html.className = 'list-group-item';

    this.selected = id;
    this.selectList.emit(id);
  }

}
