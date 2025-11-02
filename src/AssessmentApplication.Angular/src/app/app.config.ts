import {
  ApplicationConfig,
  importProvidersFrom,
  inject,
  provideAppInitializer,
  provideZoneChangeDetection,
} from "@angular/core";
import { provideRouter, withComponentInputBinding } from "@angular/router";

import {
  HTTP_INTERCEPTORS,
  provideHttpClient,
  withInterceptorsFromDi,
} from "@angular/common/http";
import { provideAnimationsAsync } from "@angular/platform-browser/animations/async";
import { definePreset } from "@primeuix/themes";
import Aura from "@primeuix/themes/aura";
import { ErrorHttpInterceptor } from "@shared/interceptors";
import { EnvService } from "@shared/services";
import {
  NgxUiLoaderHttpModule,
  NgxUiLoaderModule,
  NgxUiLoaderService,
} from "ngx-ui-loader";
import { MessageService } from "primeng/api";
import { providePrimeNG } from "primeng/config";
import { routes } from "./app.routing";

const myPreset = definePreset(Aura, {
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
    provideRouter(routes, withComponentInputBinding()),
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
