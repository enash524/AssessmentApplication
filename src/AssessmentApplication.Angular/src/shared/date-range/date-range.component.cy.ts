import { signal } from "@angular/core";
import { DateRangeComponent } from "./date-range.component";
import { CalendarModule } from "primeng/calendar";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NoopAnimationsModule } from "@angular/platform-browser/animations";

function mountDateRangeComponent(label: string = null): Cypress.Chainable {
  return cy.mount(DateRangeComponent, {
    imports: [
      CalendarModule,
      CommonModule,
      FormsModule,
      NoopAnimationsModule,
      ReactiveFormsModule,
    ],
    componentProperties: {
      label: signal(label) as any,
    },
  });
}

describe(
  "DateRangeComponent",
  { viewportHeight: 1000, viewportWidth: 1200 },
  () => {
    it("should mount", () => {
      mountDateRangeComponent();
    });
    it("should properly set labels", () => {
      const label: string = "Test Date";
      const errorMessage: string = `${label} End cannot occur before ${label} Start`;
      const fromPlaceholder: string = `${label} Start`;
      const toPlaceholder: string = `${label} End`;

      mountDateRangeComponent(label).then((wrapper) => {
        expect(wrapper.component.errorMessage()).to.equal(errorMessage);
        expect(wrapper.component.placeholderFrom()).to.equal(fromPlaceholder);
        expect(wrapper.component.placeholderTo()).to.equal(toPlaceholder);
        cy.get("dt").should("contain.text", label);
        cy.get(`input[name="${wrapper.component.id}-fromDate"]`)
          .invoke("attr", "placeholder")
          .then((placeholder) => expect(placeholder).to.equal(fromPlaceholder));
        cy.get(`input[name="${wrapper.component.id}-toDate"]`)
          .invoke("attr", "placeholder")
          .then((placeholder) => expect(placeholder).to.equal(toPlaceholder));
      });
    });
    it("should be invalid when 'To' date occurs before 'From' date", () => {
      const label: string = "Test Date";
      const errorMessage: string = `${label} End cannot occur before ${label} Start`;
      const fromDate: string = "01/01/1980";
      const toDate: string = "01/01/1970";
      mountDateRangeComponent(label).then(() => {
        cy.get("[formcontrolname='fromDate'] input")
          .type(fromDate)
          .type("{enter}");
        cy.get("[formcontrolname='toDate'] input").type(toDate).type("{enter}");
        cy.get("dt").should("have.class", "error-message");
        cy.get("dd.error-message")
          .should("exist")
          .and("contain.text", errorMessage);
      });
    });
    it("should be valid when 'To' date occurs after 'From' Date", () => {
      const label: string = "Test Date";
      const fromDate: string = "01/01/1970";
      const toDate: string = "01/01/1980";
      mountDateRangeComponent(label).then(() => {
        cy.get("[formcontrolname='fromDate'] input")
          .type(fromDate)
          .type("{enter}");
        cy.get("[formcontrolname='toDate'] input").type(toDate).type("{enter}");
        cy.get("dt").should("not.have.class", "error-message");
        cy.get("dd.error-message").should("not.exist");
      });
    });
  }
);
