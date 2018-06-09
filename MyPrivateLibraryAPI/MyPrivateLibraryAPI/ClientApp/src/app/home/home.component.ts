import { Component, OnInit } from '@angular/core';
import { RegisterFormModel } from '../models/RegisterFormModel';
import { LoginFormModel } from '../models/LoginFormModel';
import { AuthService } from '../services/auth.service';
import { AlertService } from '../services/alert.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  registerData: RegisterFormModel;
  loginData: LoginFormModel;

  constructor(private authService: AuthService,
    private alertService: AlertService,
    private router: Router) {
  }

  ngOnInit() {
    if (this.authService.isAuthenticated) {
      this.router.navigate(['/dashboard']);
    }

    this.registerData = new RegisterFormModel();
    this.loginData = new LoginFormModel();
  }

  logIn() {
    this.authService.logIn(this.loginData);
  }

  register() {
    this.authService.register(this.registerData);
  }
}
