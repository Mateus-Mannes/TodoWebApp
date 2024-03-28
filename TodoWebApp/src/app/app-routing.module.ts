import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateUserComponent } from './create-user/create-user.component';
import { LoginComponent } from './login/login.component';
import { ApplicationComponent } from './application/application.component';

const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'user', component: CreateUserComponent},
  {path: '', component: ApplicationComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
