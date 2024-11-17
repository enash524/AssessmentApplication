import { CommonModule } from "@angular/common";
import { InputTextboxComponent } from "./input-textbox.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { signal } from "@angular/core";

describe(
  "InputTextboxComponent",
  { viewportHeight: 1000, viewportWidth: 1200 },
  () => {
    it("should mount", () => {
      cy.mount(InputTextboxComponent, {
        imports: [CommonModule, FormsModule, ReactiveFormsModule],
        componentProperties: {
          label: signal("Test") as any,
        },
      });
    });
  }
);
