import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  nameControl = new FormControl('', [Validators.required]);
  passwordControl = new FormControl('', [Validators.required]);
  form: FormGroup;
  loginError = false;

  constructor(private readonly http: HttpClient) {
    this.form = new FormGroup([this.nameControl, this.passwordControl]);
   }

  ngOnInit(): void {
  }

  login() {
    this.http.post('account/login', {name: this.nameControl.value,password: this.passwordControl.value})
    .subscribe({
      next: value => {
        console.log(value);
      }, error: err => {
        this.loginError = true;
      }
    })
  }

}
