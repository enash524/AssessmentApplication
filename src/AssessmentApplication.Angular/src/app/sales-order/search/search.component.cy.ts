import { CommonModule } from "@angular/common";
import { provideHttpClientTesting } from "@angular/common/http/testing";
import { provideLocationMocks } from "@angular/common/testing";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NoopAnimationsModule } from "@angular/platform-browser/animations";
import { provideRouter, RouterModule } from "@angular/router";
import Aura from "@primeuix/themes/aura";
import { DateRangeComponent } from "@shared/date-range/date-range.component";
import { InputTextboxComponent } from "@shared/input-textbox/input-textbox.component";
import { FullAddressPipe } from "@shared/pipes/full-address.pipe";
import { EnvService } from "@shared/services";
import { EnvServiceStub, SalesOrderSearchServiceStub } from "@testing/stubs";
import { ButtonModule } from "primeng/button";
import { providePrimeNG } from "primeng/config";
import { TableModule } from "primeng/table";
import { routes } from "../sales-order.routing";
import { SalesOrderSearchService } from "../sales-order.service";
import { SearchComponent } from "./search.component";

function mountSearchComponent(): Cypress.Chainable {
  return cy.mount(SearchComponent, {
    imports: [
      ButtonModule,
      CommonModule,
      DateRangeComponent,
      FormsModule,
      InputTextboxComponent,
      NoopAnimationsModule,
      ReactiveFormsModule,
      RouterModule,
      TableModule,
    ],
    providers: [
      FullAddressPipe,
      provideHttpClientTesting(),
      provideRouter(routes),
      provideLocationMocks(),
      providePrimeNG({
        theme: {
          preset: Aura,
          options: {
            darkModeSelector: false || "none",
          },
        },
      }),
      {
        provide: EnvService,
        useClass: EnvServiceStub,
      },
      {
        provide: SalesOrderSearchService,
        useClass: SalesOrderSearchServiceStub,
      },
    ],
  });
}

describe(
  "SearchComponent",
  { viewportHeight: 1000, viewportWidth: 1200 },
  () => {
    it("should mount", () => {
      mountSearchComponent();
    });
  }
);
