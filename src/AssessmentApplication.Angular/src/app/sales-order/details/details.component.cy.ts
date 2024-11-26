import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { DetailsComponent } from "./details.component";
import { provideRouter } from "@angular/router";
import { NoopAnimationsModule } from "@angular/platform-browser/animations";
import { provideHttpClientTesting } from "@angular/common/http/testing";
import { provideLocationMocks } from "@angular/common/testing";
import { SalesOrderSearchService } from "../sales-order.service";
import { EnvServiceStub, SalesOrderSearchServiceStub } from "@testing/stubs";
import { routes } from "../sales-order.routing";
import { RouterTestingHarness } from "@angular/router/testing";
import { CommonModule } from "@angular/common";
import { SalesOrderInfoComponent } from "../widgets/sales-order-info/sales-order-info.component";
import { provideHttpClient } from "@angular/common/http";
import { EnvService } from "@shared/services";

function mountDetailsComponent(): Cypress.Chainable {
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
  "DetailsComponent",
  { viewportHeight: 1000, viewportWidth: 1200 },
  () => {
    it("should mount", () => {
      mountDetailsComponent();
    });
    it("should display data", () => {
      mountDetailsComponent().then(async (wrapper) => {
        await RouterTestingHarness.create("/detail/5");
        wrapper.fixture.detectChanges();
        cy.get("app-details h2").should("contain.text", "Sales Order Detail");
        cy.get("app-sales-order-info").should("exist").and("have.length", 5);
      });
    });
    it("should not display data", () => {
      mountDetailsComponent().then(async (wrapper) => {
        await RouterTestingHarness.create("/detail/0");
        wrapper.fixture.detectChanges();
        cy.get("app-sales-order-info").should("not.exist");
        cy.get("app-details").within(() => {
          cy.get("h2").should("contain.text", "Sales Order Detail");
          cy.get("div.col-md-12").should("contain.text", "No records found");
        });
      });
    });
  }
);
