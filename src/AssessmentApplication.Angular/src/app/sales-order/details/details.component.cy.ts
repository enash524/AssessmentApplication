import { CommonModule } from "@angular/common";
import { provideHttpClient } from "@angular/common/http";
import { provideHttpClientTesting } from "@angular/common/http/testing";
import { provideLocationMocks } from "@angular/common/testing";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NoopAnimationsModule } from "@angular/platform-browser/animations";
import { provideRouter } from "@angular/router";
import Aura from "@primeuix/themes/aura";
import { EnvService } from "@shared/services";
import { EnvServiceStub, SalesOrderSearchServiceStub } from "@testing/stubs";
import { providePrimeNG } from "primeng/config";
import { routes } from "../sales-order.routing";
import { SalesOrderSearchService } from "../sales-order.service";
import { SalesOrderInfoComponent } from "../widgets/sales-order-info/sales-order-info.component";
import { DetailsComponent } from "./details.component";

function mountDetailsComponent(id: number | null = null): Cypress.Chainable {
  return cy.mount(DetailsComponent, {
    imports: [
      CommonModule,
      FormsModule,
      NoopAnimationsModule,
      ReactiveFormsModule,
      SalesOrderInfoComponent,
    ],
    providers: [
      provideHttpClient(),
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
    componentProperties: {
      id: id,
    },
  });
}

describe(
  "DetailsComponent",
  { viewportHeight: 1000, viewportWidth: 1200 },
  () => {
    it("should mount", () => {
      mountDetailsComponent();
    });
    it("should display data", () => {
      mountDetailsComponent(5);
      cy.get("h2").should("contain.text", "Sales Order Detail");
      cy.get("app-sales-order-info").should("exist").and("have.length", 5);
    });
    it("should not display data", () => {
      mountDetailsComponent(0);
      cy.get("app-sales-order-info").should("not.exist");
      cy.get("h2").should("contain.text", "Sales Order Detail");
      cy.get("div.col-md-12").should("contain.text", "No records found");
    });
  }
);
