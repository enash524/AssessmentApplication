import { APP_INITIALIZER, NgModule } from "@angular/core";
import { BrowserModule, Title } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { ErrorHttpInterceptor } from "@shared/interceptors/error.interceptor";
import { AppRoutingModule } from "./app.routing";
import { AppComponent } from "./app.component";
import { SalesOrderComponent } from "./sales-order/sales-order.component";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";
import { EnvService } from "@shared/services/env.service";
import {
  NgxUiLoaderHttpModule,
  NgxUiLoaderModule,
  NgxUiLoaderRouterModule,
} from "ngx-ui-loader";
import { MessageService } from "primeng/api";
import { TableModule } from "primeng/table";

@NgModule({ declarations: [AppComponent, SalesOrderComponent],
    bootstrap: [AppComponent], imports: [BrowserModule,
        BrowserAnimationsModule,
        AppRoutingModule,
        FontAwesomeModule,
        FormsModule,
        NgxUiLoaderModule.forRoot({ fgsType: "rectangle-bounce" }),
        NgxUiLoaderHttpModule.forRoot({ showForeground: true }),
        NgxUiLoaderRouterModule,
        ReactiveFormsModule,
        TableModule], providers: [
        MessageService,
        Title,
        {
            provide: APP_INITIALIZER,
            useFactory: (envService: EnvService) => () => envService.init(),
            deps: [EnvService],
            multi: true,
        },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: ErrorHttpInterceptor,
            multi: true,
        },
        provideHttpClient(withInterceptorsFromDi()),
    ] })
export class AppModule {}
