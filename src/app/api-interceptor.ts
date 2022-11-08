import {Injectable} from '@angular/core';
import {HttpEvent, HttpInterceptor, HttpHandler, HttpRequest} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth-service';
import { Router } from '@angular/router';

@Injectable()
export class APIInterceptor implements HttpInterceptor {

  constructor(private readonly authService: AuthService,
    private readonly router: Router){}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let apiReq = req.clone({ url: `https://localhost:7259/${req.url}` });
    const token = localStorage.getItem("id_token");
    if (req.url != 'account/login' && (!token || !this.authService.isLoggedIn())) {
      this.router.navigateByUrl('/login');
    }
    apiReq = apiReq.clone({
      headers: req.headers.set("Authorization",
          "Bearer " + token)
    });
    return next.handle(apiReq);
  }
}