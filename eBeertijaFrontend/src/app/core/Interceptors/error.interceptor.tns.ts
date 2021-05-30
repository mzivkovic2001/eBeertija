import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthenticationService } from '../Services/authentication.service';
import * as dialogs from '@nativescript/core/ui/dialogs';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private authenticationService: AuthenticationService) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            if (err.status === 401) {
                // auto logout if 401 response returned from api
                this.authenticationService.logout();
                location.reload(true);
            }
            console.log(err);
            //console.log(err.error.message || err.statusText);
            dialogs.alert({
                title: "Pogreška",
                message: err.error,
                okButtonText: "OK"}
                )
            //const error = err.error.message || err.statusText;
            return throwError(err);
        }));
    }
}
