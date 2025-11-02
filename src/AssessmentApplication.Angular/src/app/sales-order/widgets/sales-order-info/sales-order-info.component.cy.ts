import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { SalesOrderDetail } from "@app/sales-order/models";
import { SalesOrderInfoComponent } from "./sales-order-info.component";

function mountSalesOrderInfoComponent(
  salesOrderDetail: SalesOrderDetail | null = null
): Cypress.Chainable {
  return cy.mount(SalesOrderInfoComponent, {
    imports: [CommonModule, FormsModule, ReactiveFormsModule],
    componentProperties: {
      salesOrderDetail: salesOrderDetail,
    },
  });
}

describe(
  "SalesOrderInfoComponent",
  { viewportHeight: 1000, viewportWidth: 1200 },
  () => {
    it("should mount", () => {
      mountSalesOrderInfoComponent();
    });
    it("should display data", () => {
      const expectedSalesOrderDetail = [
        {
          label: "Product Number:",
          value: "ABC123",
        },
        {
          label: "Order Quantity:",
          value: "1",
        },
        {
          label: "Unit Price:",
          value: "$1.23",
        },
        {
          label: "Unit Price Discount:",
          value: "$0.10",
        },
        {
          label: "Line Total:",
          value: "$1.00",
        },
      ];
      cy.fixture("sales-order/detail-5-info.json").then(
        (salesOrderDetail: SalesOrderDetail) => {
          mountSalesOrderInfoComponent(salesOrderDetail);
          cy.get("div.card").within(() => {
            cy.get("div.card-header").should("contain.text", "Test 5");
            cy.get("div.card-body dl").within(() => {
              cy.get("dt").should("have.length", 5);
              cy.get("dd").should("have.length", 5);

              expectedSalesOrderDetail.forEach((item, index) => {
                cy.get("dt").eq(index).should("contain.text", item.label);
                cy.get("dd").eq(index).should("contain.text", item.value);
              });
            });
          });
        }
      );
    });
  }
);
