import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule, Title } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ErrorHttpInterceptor } from '@shared/interceptors/error.interceptor';
import { AppRoutingModule } from './app.routing';
import { AppComponent } from './app.component';
import { SalesOrderComponent } from './sales-order/sales-order.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome'
import { EnvService } from '@shared/services/env.service';
import { HomePageComponent } from './home-page/home-page.component';
import { NgxUiLoaderHttpModule, NgxUiLoaderModule, NgxUiLoaderRouterModule } from 'ngx-ui-loader';
import { MessageService } from 'primeng/api';
import { SharedModule } from '@shared/shared.module';
import { TableModule } from 'primeng/table';

@NgModule({
  declarations: [
    AppComponent,
    SalesOrderComponent,
    HomePageComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    FontAwesomeModule,
    FormsModule,
    HttpClientModule,
    NgxUiLoaderModule.forRoot({ 'fgsType': 'rectangle-bounce' }),
    NgxUiLoaderHttpModule.forRoot({ 'showForeground': true }),
    NgxUiLoaderRouterModule,
    ReactiveFormsModule,
    SharedModule,
    TableModule,
  ],
  providers: [
    MessageService,
    Title,
    {
      provide: APP_INITIALIZER,
      useFactory: (envService: EnvService) => () => envService.init(),
      deps: [
        EnvService,
      ],
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorHttpInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
