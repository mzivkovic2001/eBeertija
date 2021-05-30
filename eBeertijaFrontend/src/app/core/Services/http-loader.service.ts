import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class HttpLoaderService {
    activeRequests$: BehaviorSubject<number>;
    isLoading$: Observable<boolean>;

    constructor() {
        this.activeRequests$ = new BehaviorSubject(0);
        this.isLoading$ = this.activeRequests$.pipe(
            map(requests => requests > 0)
        );
    }

    public onRequestStart() {
        setTimeout(() => this.activeRequests$.next(this.activeRequests$.value + 1), 10);
    }

    public onRequestEnd() {
        setTimeout(() => this.activeRequests$.next(this.activeRequests$.value - 1), 10);
    }
}
