import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NoopAnimationsModule } from "@angular/platform-browser/animations";
import Aura from "@primeuix/themes/aura";
import { providePrimeNG } from "primeng/config";
import { InputTextboxComponent } from "./input-textbox.component";

function mountInputTextboxComponent(label: string = null): Cypress.Chainable {
  return cy.mount(InputTextboxComponent, {
    imports: [
      CommonModule,
      FormsModule,
      NoopAnimationsModule,
      ReactiveFormsModule,
    ],
    providers: [
      providePrimeNG({
        theme: {
          preset: Aura,
          options: {
            darkModeSelector: false || "none",
          },
        },
      }),
    ],
    componentProperties: {
      label: label,
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
