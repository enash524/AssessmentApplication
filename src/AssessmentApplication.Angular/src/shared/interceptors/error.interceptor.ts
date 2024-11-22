import { inject, Injectable } from "@angular/core";
import {
  HttpErrorResponse,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from "@angular/common/http";
import { NgxUiLoaderService } from "ngx-ui-loader";
import { tap } from "rxjs/operators";
import { MessageService } from "primeng/api";

@Injectable()
export class ErrorHttpInterceptor implements HttpInterceptor {
  private messageService = inject(MessageService);
  private ngxService = inject(NgxUiLoaderService);

  public intercept(req: HttpRequest<any>, next: HttpHandler) {
    return next.handle(req.clone()).pipe(
      tap({
        next: () => {},
        error: (httpErrorResponse: HttpErrorResponse) => {
          this.messageService.add({
            severity: "error",
            summary: "Error",
            detail: httpErrorResponse.message,
            sticky: true,
          });
        },
      })
    );
  }
}
