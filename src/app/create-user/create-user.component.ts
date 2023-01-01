import { AlertService } from './../alert-service';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../auth-service';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['../login/login.component.css']
})
export class CreateUserComponent implements OnInit {

  nameControl = new FormControl('', [Validators.required]);
  passwordControl = new FormControl('', [Validators.required]);
  form: FormGroup;
  loading = false;

  constructor(private readonly http: HttpClient,
    private readonly router: Router,
    private readonly alertService: AlertService) {
    this.form = new FormGroup([this.nameControl, this.passwordControl]);
   }

  ngOnInit(): void {
  }

  create() {
    this.loading = true;
    this.http.post('account', {name: this.nameControl.value, password: this.passwordControl.value})
    .subscribe({
      next: value => {
        this.router.navigateByUrl('/login');
        this.alertService.alert('account created', 'success')
      }, error: err => {
        this.loading = false;
        this.alertService.notifyErros(err);
      }
    })
  }

}
