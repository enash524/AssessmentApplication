import { CommonModule } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { Title } from "@angular/platform-browser";
import { RouterLink, RouterLinkActive, RouterOutlet } from "@angular/router";
import { NgxUiLoaderModule, NgxUiLoaderRouterModule } from "ngx-ui-loader";

@Component({
  selector: "app-root",
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    RouterOutlet,
    NgxUiLoaderModule,
    NgxUiLoaderRouterModule,
  ],
  providers: [Title],
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
})
export class AppComponent implements OnInit {
  public title: string = "Assessment Application";

  public constructor(private titleService: Title) {}

  public ngOnInit(): void {
    this.setTitle(this.title);
  }

  public setTitle(newTitle: string) {
    this.titleService.setTitle(newTitle);
  }
}
