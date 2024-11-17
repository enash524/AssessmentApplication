import {
  APP_INITIALIZER,
  ApplicationConfig,
  importProvidersFrom,
  provideZoneChangeDetection,
} from "@angular/core";
import { provideRouter } from "@angular/router";

import { routes } from "./app.routing";
import {
  HTTP_INTERCEPTORS,
  provideHttpClient,
  withInterceptorsFromDi,
} from "@angular/common/http";
import { provideAnimations } from "@angular/platform-browser/animations";
import {
  NgxUiLoaderHttpModule,
  NgxUiLoaderModule,
  NgxUiLoaderService,
} from "ngx-ui-loader";
import { EnvService } from "@shared/services";
import { ErrorHttpInterceptor } from "@shared/interceptors";
import { MessageService } from "primeng/api";

export const appConfig: ApplicationConfig = {
  providers: [
    provideAnimations(),
    provideHttpClient(withInterceptorsFromDi()),
    provideRouter(routes),
    provideZoneChangeDetection({ eventCoalescing: true }),
    importProvidersFrom([
      NgxUiLoaderModule.forRoot({ fgsType: "rectangle-bounce" }),
      NgxUiLoaderHttpModule.forRoot({ showForeground: true }),
    ]),
    MessageService,
    NgxUiLoaderService,
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
  ],
};
