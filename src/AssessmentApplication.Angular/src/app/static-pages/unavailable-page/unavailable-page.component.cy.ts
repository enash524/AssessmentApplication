import { UnavailablePageComponent } from "./unavailable-page.component";

describe(
  "UnavailablePageComponent",
  { viewportHeight: 1000, viewportWidth: 1200 },
  () => {
    it("should mount", () => {
      cy.mount(UnavailablePageComponent);
    });
  }
);
