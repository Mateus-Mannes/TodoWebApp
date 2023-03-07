import { MatIconModule } from '@angular/material/icon';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateUserComponent } from './create-user/create-user.component';
import { GroupsComponent } from './groups/groups/groups.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'user', component: CreateUserComponent},
  {path: 'groups', component: GroupsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes), MatIconModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
