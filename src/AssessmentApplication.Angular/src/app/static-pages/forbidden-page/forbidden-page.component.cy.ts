import { ForbiddenPageComponent } from "./forbidden-page.component";

describe(
  "ForbiddenPageComponent",
  { viewportHeight: 1000, viewportWidth: 1200 },
  () => {
    it("should mount", () => {
      cy.mount(ForbiddenPageComponent);
    });
  }
);
