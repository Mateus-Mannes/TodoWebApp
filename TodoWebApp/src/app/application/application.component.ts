import { Router } from '@angular/router';
import { GridComponent } from './grid/grid/grid.component';
import { GroupsComponent } from './groups/groups/groups.component';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { TodoGroup } from '../shared/entities/todo-group';
import { AlertService } from '../shared/services/alert-service';

@Component({
  selector: 'app-application',
  templateUrl: './application.component.html',
  styleUrls: ['./application.component.css']
})
export class ApplicationComponent implements OnInit {

  loading = true;
  groups: TodoGroup[];

  constructor(private readonly _httpClient: HttpClient,
    private readonly _alertService: AlertService,
    private  readonly _router : Router) {
    _httpClient.get<TodoGroup[]>('todo-group').subscribe({
      next: res => { this.groups = res; this.loading = false},
      error: value => {
        if(this._router.url != '/login')
          _alertService.alert(`Error on loading todos - ${value.error}`, 'danger'
      );
      this.loading = false}
    });
  }

  ngOnInit(): void {
  }

}
