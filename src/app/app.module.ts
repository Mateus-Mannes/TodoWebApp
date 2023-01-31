import { AlertService } from './alert-service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { APIInterceptor } from './api-interceptor';
import {MatTableModule} from '@angular/material/table';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { GridComponent } from './grid/grid.component';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import {MatIconModule} from '@angular/material/icon';
import { EditComponent } from './edit/edit.component';
import {MatDialogModule} from '@angular/material/dialog';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatNativeDateModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import { LoginComponent } from './login/login.component';
import { TodoListComponent } from './todo-list/todo-list.component';
import { AuthService } from './auth-service';
import { CreateUserComponent } from './create-user/create-user.component';
import { GroupsComponent } from './groups/groups.component';
import { ApplicationComponent } from './application/application.component';
import { GroupDialogComponent } from './group-dialog/group-dialog.component';
import { InputComponent } from './input/input.component';

@NgModule({
  declarations: [
    AppComponent,
    GridComponent,
    EditComponent,
    LoginComponent,
    TodoListComponent,
    CreateUserComponent,
    GroupsComponent,
    ApplicationComponent,
    GroupDialogComponent,
    InputComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MatTableModule,
    BrowserAnimationsModule,
    MatCheckboxModule,
    FormsModule,
    ReactiveFormsModule,
    MatIconModule,
    MatDialogModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatInputModule,
    MatButtonModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: APIInterceptor,
    multi: true,
  }, AuthService, AlertService],
  bootstrap: [AppComponent]
})
export class AppModule { }
