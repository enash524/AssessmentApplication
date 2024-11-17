import { CommonModule } from "@angular/common";
import { SalesOrderInfoComponent } from "./sales-order-info.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { signal } from "@angular/core";

describe(
  "SalesOrderInfoComponent",
  { viewportHeight: 1000, viewportWidth: 1200 },
  () => {
    it("should mount", () => {
      cy.mount(SalesOrderInfoComponent, {
        imports: [CommonModule, FormsModule, ReactiveFormsModule],
        componentProperties: {
          salesOrderDetail: signal(null) as any,
        },
      });
    });
  }
);
