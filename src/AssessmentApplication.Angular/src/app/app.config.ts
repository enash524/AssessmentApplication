import {
  ApplicationConfig,
  importProvidersFrom,
  provideZoneChangeDetection,
  inject,
  provideAppInitializer,
} from "@angular/core";
import { provideRouter } from "@angular/router";

import { routes } from "./app.routing";
import {
  HTTP_INTERCEPTORS,
  provideHttpClient,
  withInterceptorsFromDi,
} from "@angular/common/http";
import { provideAnimationsAsync } from "@angular/platform-browser/animations/async";
import {
  NgxUiLoaderHttpModule,
  NgxUiLoaderModule,
  NgxUiLoaderService,
} from "ngx-ui-loader";
import { EnvService } from "@shared/services";
import { ErrorHttpInterceptor } from "@shared/interceptors";
import { providePrimeNG } from "primeng/config";
import { MessageService } from "primeng/api";
import Lara from "@primeng/themes/lara";
import { definePreset } from "@primeng/themes";

const myPreset = definePreset(Lara, {
  semantic: {
    primary: {
      50: "{blue.50}",
      100: "{blue.100}",
      200: "{blue.200}",
      300: "{blue.300}",
      400: "{blue.400}",
      500: "{blue.500}",
      600: "{blue.600}",
      700: "{blue.700}",
      800: "{blue.800}",
      900: "{blue.900}",
      950: "{blue.950}",
    },
  },
});

export const appConfig: ApplicationConfig = {
  providers: [
    provideAnimationsAsync(),
    provideHttpClient(withInterceptorsFromDi()),
    provideRouter(routes),
    provideZoneChangeDetection({ eventCoalescing: true }),
    providePrimeNG({
      theme: {
        preset: myPreset,
        options: {
          darkModeSelector: false || "none",
        },
      },
    }),
    importProvidersFrom([
      NgxUiLoaderModule.forRoot({ fgsType: "rectangle-bounce" }),
      NgxUiLoaderHttpModule.forRoot({ showForeground: true }),
    ]),
    MessageService,
    NgxUiLoaderService,
    provideAppInitializer(() => {
      const initializerFn = (
        (envService: EnvService) => () =>
          envService.init()
      )(inject(EnvService));
      return initializerFn();
    }),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorHttpInterceptor,
      multi: true,
    },
  ],
};
