import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDatepicker } from '@angular/material/datepicker';
import { MatInput } from '@angular/material/input';
import { raceWith } from 'rxjs';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.css']
})
export class InputComponent implements OnInit {

  @ViewChild('picker') datepicker: MatDatepicker<Date | null>;

  constructor() { }

  ngOnInit(): void {
  }

  discard(){
    this.datepicker.select(null);
    this.datepicker.close();
  }

}
