import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule, Title } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

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
    BrowserAnimationsModule,
    AppRoutingModule,
    FontAwesomeModule,
    ReactiveFormsModule,
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
