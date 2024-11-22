import { Component, DestroyRef, inject, OnInit, signal } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { of } from "rxjs";
import { map, switchMap } from "rxjs/operators";
import { SalesOrderDetail } from "@app/sales-order/models";
import { SalesOrderSearchService } from "@app/sales-order";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { SalesOrderInfoComponent } from "../widgets/sales-order-info/sales-order-info.component";
import { takeUntilDestroyed } from "@angular/core/rxjs-interop";

@Component({
  selector: "app-details",
  templateUrl: "./details.component.html",
  styleUrls: ["./details.component.scss"],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SalesOrderInfoComponent,
  ],
})
export class DetailsComponent implements OnInit {
  public salesOrderDetails = signal<SalesOrderDetail[]>(null);
  private activatedRoute = inject(ActivatedRoute);
  private destroyRef = inject(DestroyRef);
  private salesOrderSearchService = inject(SalesOrderSearchService);

  ngOnInit(): void {
    this.activatedRoute.paramMap
      .pipe(
        map((params) => {
          const param = params.get("id");
          return param ? +param : null;
        }),
        switchMap((id) => (id ? this.salesOrderSearchService.get(id) : of([]))),
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe((details: SalesOrderDetail[]) =>
        this.salesOrderDetails.set(details)
      );
  }
}
