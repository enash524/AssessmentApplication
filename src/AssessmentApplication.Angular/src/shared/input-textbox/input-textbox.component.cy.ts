import { CommonModule } from "@angular/common";
import { InputTextboxComponent } from "./input-textbox.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { signal } from "@angular/core";
import { NoopAnimationsModule } from "@angular/platform-browser/animations";

function mountInputTextboxComponent(label: string = null): Cypress.Chainable {
  return cy.mount(InputTextboxComponent, {
    imports: [
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
  "InputTextboxComponent",
  { viewportHeight: 1000, viewportWidth: 1200 },
  () => {
    it("should mount", () => {
      mountInputTextboxComponent();
    });
    it("should properly set labels", () => {
      const label: string = "Test Name";
      mountInputTextboxComponent(label).then((wrapper) => {
        cy.get("dt").should("contain.text", label);
        cy.get("input[formcontrolname='textboxValue']")
          .invoke("attr", "placeholder")
          .then((placeholder) => expect(placeholder).to.equal(label));
      });
    });
  }
);
