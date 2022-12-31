import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertService } from '../alert-service';
import { AuthService } from '../auth-service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  nameControl = new FormControl('', [Validators.required]);
  passwordControl = new FormControl('', [Validators.required]);
  form: FormGroup;
  loading = false;

  constructor(private readonly http: HttpClient,
    private readonly router: Router,
    private readonly authService: AuthService,
    private readonly alertService: AlertService) {
    this.form = new FormGroup([this.nameControl, this.passwordControl]);
   }

  ngOnInit(): void {
  }

  login() {
    this.loading = true;
    this.authService.login(this.nameControl.value ?? '',this.passwordControl.value ?? '')
    .subscribe({
      next: value => {
        this.authService.setSession(value);
        this.router.navigateByUrl('/');
      }, error: err => {
        this.loading = false;
        this.alertService.alert('login failed', 'danger');
      }
    })
  }

}
