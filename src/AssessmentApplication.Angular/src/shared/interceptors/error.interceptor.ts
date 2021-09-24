import { Injectable } from '@angular/core';
import { HttpErrorResponse, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { tap } from 'rxjs/operators';


@Injectable()
export class ErrorHttpInterceptor implements HttpInterceptor {

    constructor (
        private ngxService: NgxUiLoaderService
    ) {}

    public intercept(req: HttpRequest<any>, next: HttpHandler) {
        return next.handle(req.clone()).pipe(tap(() => {},
            (httpErrorResponse: HttpErrorResponse) => {
                
            }
        ))
    }

}
