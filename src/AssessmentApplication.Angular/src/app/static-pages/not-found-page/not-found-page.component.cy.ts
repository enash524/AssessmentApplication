import { NotFoundPageComponent } from "./not-found-page.component";

describe(
  "NotFoundPageComponent",
  { viewportHeight: 1000, viewportWidth: 1200 },
  () => {
    it("should mount", () => {
      cy.mount(NotFoundPageComponent);
    });
  }
);
