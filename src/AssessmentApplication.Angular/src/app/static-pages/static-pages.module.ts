import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { NotFoundPageComponent } from "./not-found-page/not-found-page.component";
import { ForbiddenPageComponent } from "./forbidden-page/forbidden-page.component";
import { UnavailablePageComponent } from "./unavailable-page/unavailable-page.component";

@NgModule({
  declarations: [
    NotFoundPageComponent,
    ForbiddenPageComponent,
    UnavailablePageComponent,
  ],
  imports: [CommonModule],
})
export class StaticPagesModule {}
