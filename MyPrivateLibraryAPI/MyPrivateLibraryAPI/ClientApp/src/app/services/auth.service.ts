import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { LoginFormModel } from '../models/LoginFormModel';
import { RegisterFormModel } from '../models/RegisterFormModel';
import { Router } from '@angular/router';
import { AlertService } from './alert.service';

@Injectable()
export class AuthService {

    constructor(private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        public router: Router,
        private alertService: AlertService
    ) {
        this.baseUrl += 'api/account';
    }

    TOKEN_KEY = 'token';

    get token() {
        return localStorage.getItem(this.TOKEN_KEY);
    }

    get isAuthenticated(): boolean {
        return !!localStorage.getItem(this.TOKEN_KEY);
    }

    logIn(loginData: LoginFormModel) {
        this.http.post(this.baseUrl + '/login',
            {
                email: loginData.email,
                password: loginData.password
            }).subscribe(
                res => {
                    this.saveToken(res['token']);
                    if (this.token === 'undefined') {
                        this.saveToken(res);
                    }
                    this.router.navigate(['/dashboard']);
                },
                err => {
                    console.warn(err);
                    this.alertService.error('Invalid data!');
                });
    }

    register(registerData: RegisterFormModel) {
        return this.http.post(this.baseUrl + '/register',
            {
                email: registerData.email,
                password: registerData.password,
                lastname: registerData.lastname,
                firstname: registerData.firstname
            }).subscribe(res => {
                this.saveToken(res['token']);
                if (this.token === 'undefined') {
                    this.saveToken(res);
                }
                this.router.navigate(['/dashboard']);
            },
                err => {
                    this.alertService.error('Invalid data!');
                });
    }

    logOut() {
        localStorage.removeItem(this.TOKEN_KEY);
        this.router.navigate(['/home']);
    }

    saveToken(token) {
        localStorage.setItem(this.TOKEN_KEY, token);
    }
}
