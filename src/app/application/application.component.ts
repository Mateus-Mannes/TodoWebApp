import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { TodoGroup } from '../entities/todo-group';

@Component({
  selector: 'app-application',
  templateUrl: './application.component.html',
  styleUrls: ['./application.component.css']
})
export class ApplicationComponent implements OnInit, AfterViewInit {

  constructor(private readonly http: HttpClient) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {

  }

  error(msg: string) {

  }

  selectList(id: number) {

  }

}
