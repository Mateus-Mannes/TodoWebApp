import { MatIconModule } from '@angular/material/icon';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GroupsComponent } from './groups/groups.component';



@NgModule({
  declarations: [
    GroupsComponent
  ],
  imports: [
    CommonModule,
    MatIconModule
  ]
})
export class GroupsModule { }
