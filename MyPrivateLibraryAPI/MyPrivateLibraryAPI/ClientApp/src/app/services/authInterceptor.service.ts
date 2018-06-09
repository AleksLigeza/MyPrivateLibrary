import { Injectable, Injector } from '@angular/core';
import { HttpInterceptor, HttpErrorResponse, HttpEvent, HttpRequest } from '@angular/common/http';
import 'rxjs/add/operator/do';
import { AuthService } from './auth.service';

@Injectable()
export class AuthInterceptorService implements HttpInterceptor {

    constructor(private injector: Injector) { }

    intercept(req, next) {
        const auth = this.injector.get(AuthService);
        const authRequest = req.clone({
            headers: req.headers.set('Authorization', 'token ' + auth.token)
        });
        return next.handle(authRequest).do(event => { }, err => {
            if (err instanceof HttpErrorResponse && err.status === 401) {
                if (auth.isAuthenticated) {
                    auth.logOut();
                }
            } else if (err instanceof HttpErrorResponse && err.status === 404) {
                auth.router.navigate(['/error']);
            }
        });
    }
}
