import { Injectable } from '@angular/core';
import { HttpErrorResponse, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { tap } from 'rxjs/operators';
import { MessageService } from 'primeng/api';


@Injectable()
export class ErrorHttpInterceptor implements HttpInterceptor {

    constructor (
        private messageService: MessageService,
        private ngxService: NgxUiLoaderService,
    ) { }

    public intercept(req: HttpRequest<any>, next: HttpHandler) {
        return next.handle(req.clone()).pipe(tap(() => {},
            (httpErrorResponse: HttpErrorResponse) => {
                this.messageService.add({
                    severity: 'error',
                    summary: 'Error',
                    detail: httpErrorResponse.message,
                    sticky: true,
                });
            }
        ));
    }

}
