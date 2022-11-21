import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth-service';
import { TodoGroup } from '../entities/todo-group';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {
  groups: TodoGroup[];
  @Output() error: EventEmitter<string> = new EventEmitter<string>();

  constructor(private readonly authService: AuthService,
    private readonly router: Router,
    private readonly http: HttpClient) { }

  ngOnInit(): void {
    
  }

  logout(){
    this.authService.logout();
    this.router.navigateByUrl('/login');
  }

}
