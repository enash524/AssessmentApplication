import { ButtonModule } from "primeng/button";
import { SearchComponent } from "./search.component";
import { CommonModule } from "@angular/common";
import { DateRangeComponent } from "@shared/date-range/date-range.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { InputTextboxComponent } from "@shared/input-textbox/input-textbox.component";
import { NoopAnimationsModule } from "@angular/platform-browser/animations";
import { TableModule } from "primeng/table";
import { provideRouter, RouterModule } from "@angular/router";
import { FullAddressPipe } from "@shared/pipes/full-address.pipe";
import { provideHttpClientTesting } from "@angular/common/http/testing";
import { routes } from "../sales-order.routing";
import { provideLocationMocks } from "@angular/common/testing";
import { SalesOrderSearchService } from "../sales-order.service";
import { EnvServiceStub, SalesOrderSearchServiceStub } from "@testing/stubs";
import { EnvService } from "@shared/services";

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
