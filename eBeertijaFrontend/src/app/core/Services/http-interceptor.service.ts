import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { HttpLoaderService } from './http-loader.service';

@Injectable()
export class HttpInterceptorService implements HttpInterceptor {
    constructor(private httpLoaderService: HttpLoaderService) {
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.onStart(req.url);

        return next.handle(req).pipe(
            tap((event: HttpEvent<any>) => {
                if (event instanceof HttpResponse) {
                    this.onEnd(event.url);
                }
            }, (err: any) => {
                this.onEnd(req.url);
            }));
    }

    private onStart(url: string) {
        this.httpLoaderService.onRequestStart();
    }

    private onEnd(url: string): void {
        this.httpLoaderService.onRequestEnd();
    }
}