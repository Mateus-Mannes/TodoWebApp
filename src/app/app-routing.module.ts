import { GridComponent } from './grid/grid/grid.component';
import { MatIconModule } from '@angular/material/icon';
import { TodoComponent } from './grid/todo/todo.component';
import { InputComponent } from './input/input/input.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ApplicationComponent } from './application/application.component';
import { CreateUserComponent } from './create-user/create-user.component';
import { GroupsComponent } from './groups/groups/groups.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  { path: '', component: ApplicationComponent },
  {path: 'login', component: LoginComponent},
  {path: 'user', component: CreateUserComponent},
  {path: 'groups', component: GroupsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes), MatIconModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
