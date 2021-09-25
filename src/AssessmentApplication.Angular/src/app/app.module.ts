import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule, Title } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { ErrorHttpInterceptor } from 'src/shared/interceptors/error.interceptor';
import { AppRoutingModule } from './app.routing';
import { AppComponent } from './app.component';
import { SalesOrderComponent } from './sales-order/sales-order.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome'
import { EnvService } from './env.service';
import { HomePageComponent } from './home-page/home-page.component';

@NgModule({
  declarations: [
    AppComponent,
    SalesOrderComponent,
    HomePageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule,
  ],
  providers: [
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
