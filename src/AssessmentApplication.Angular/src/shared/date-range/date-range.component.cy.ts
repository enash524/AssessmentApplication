import { signal } from "@angular/core";
import { DateRangeComponent } from "./date-range.component";
import { CalendarModule } from "primeng/calendar";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

describe(
  "DateRangeComponent",
  { viewportHeight: 1000, viewportWidth: 1200 },
  () => {
    it("should mount", () => {
      cy.mount(DateRangeComponent, {
        imports: [
          CalendarModule,
          CommonModule,
          FormsModule,
          ReactiveFormsModule,
        ],
        componentProperties: {
          label: signal("Test") as any,
        },
      });
    });
  }
);
