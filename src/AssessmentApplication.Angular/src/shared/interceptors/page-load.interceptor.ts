import { catchError, tap } from "rxjs/operators";
import { Observable, throwError as observableThrowError } from "rxjs";
import { Injectable } from "@angular/core";
import { HttpEvent, HttpEventType, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { NgxUiLoaderService } from "ngx-ui-loader";
import { Router } from "@angular/router";
import { v4 as uuid } from 'uuid';


@Injectable()
export class PageLoadHttpInterceptor implements HttpInterceptor {

    private defaultConfig = {
        excludeRequest: [],
        excludeRoute: [],
    };

    constructor (
        private ngxService: NgxUiLoaderService,
        private router: Router,
    ) {}

    public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        if (this.defaultConfig.excludeRequest) {
            if (this.defaultConfig.excludeRequest.findIndex(url => request.url.toLowerCase().endsWith(url)) > -1) {
                return next.handle(request);
            }
        }

        if (this.defaultConfig.excludeRoute) {
            if (this.defaultConfig.excludeRoute.findIndex(url => this.router.url.toLowerCase().endsWith(url)) > -1) {
                return next.handle(request);
            }
        }

        let watchItem: string = request.url + uuid();
        this.ngxService.start(watchItem);
        return next.handle(request).pipe(tap(event => {
            if (event.type === HttpEventType.Response) {
                this.ngxService.stop(watchItem);
            }
        }), catchError(error => {
            this.ngxService.stop(watchItem);
            return observableThrowError(error);
        }), );
    }

}