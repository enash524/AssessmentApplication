import { Component, effect, inject, input } from "@angular/core";
import { Title } from "@angular/platform-browser";
import { RouterLink, RouterLinkActive, RouterOutlet } from "@angular/router";
import { NgxUiLoaderModule, NgxUiLoaderRouterModule } from "ngx-ui-loader";

@Component({
  selector: "app-root",
  imports: [
    RouterLink,
    RouterLinkActive,
    RouterOutlet,
    NgxUiLoaderModule,
    NgxUiLoaderRouterModule,
  ],
  providers: [Title],
  templateUrl: "./app.component.html",
  styleUrl: "./app.component.scss",
})
export class AppComponent {
  public title = input<string>("Assessment Application");
  private titleService = inject(Title);

  constructor() {
    effect(() => this.titleService.setTitle(this.title()));
  }
}
