import { NoopAnimationsModule } from "@angular/platform-browser/animations";
import { HomePageComponent } from "./home-page.component";

describe(
  "HomePageComponent",
  { viewportHeight: 1000, viewportWidth: 1200 },
  () => {
    it("should mount", () => {
      cy.mount(HomePageComponent, {
        imports: [NoopAnimationsModule],
      });
    });
  }
);
