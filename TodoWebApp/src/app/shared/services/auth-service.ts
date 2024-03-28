import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable()
export class AuthService {

    constructor(private http: HttpClient) {

    }

    login(name:string, password:string ) {
        return this.http.post('account/login', {name, password});
    }
          
    setSession(authResult: any) {
        localStorage.setItem('id_token', authResult.token);
        localStorage.setItem("expiration", authResult.expiresAt );
    }          

    logout() {
        localStorage.removeItem("id_token");
        localStorage.removeItem("expiration");
    }

    public isLoggedIn() {
        const now = new Date();
        const expiration = localStorage.getItem('expiration') ?? now;
        const expirationDate = new Date(expiration);
        return now < expirationDate;
    }

    isLoggedOut() {
        return !this.isLoggedIn();
    }
}
      