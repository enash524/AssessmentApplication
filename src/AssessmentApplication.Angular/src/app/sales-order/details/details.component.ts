import { Component, DestroyRef, inject, OnInit, signal } from "@angular/core";
import { ActivatedRoute, ParamMap } from "@angular/router";
import { of } from "rxjs";
import { map, switchMap } from "rxjs/operators";
import { SalesOrderDetail } from "@app/sales-order/models";
import { SalesOrderSearchService } from "@app/sales-order";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { SalesOrderInfoComponent } from "../widgets/sales-order-info/sales-order-info.component";
import { takeUntilDestroyed } from "@angular/core/rxjs-interop";

@Component({
  selector: "app-details",
  templateUrl: "./details.component.html",
  styleUrls: ["./details.component.scss"],
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, SalesOrderInfoComponent],
})
export class DetailsComponent implements OnInit {
  public salesOrderDetails = signal<SalesOrderDetail[] | null>(null);
  private activatedRoute = inject(ActivatedRoute);
  private destroyRef = inject(DestroyRef);
  private salesOrderSearchService = inject(SalesOrderSearchService);

  ngOnInit(): void {
    this.activatedRoute.paramMap
      .pipe(
        map((params: ParamMap) => {
          const param = params.get("id");
          return param !== null ? +param : null;
        }),
        switchMap((id: number | null) =>
          id !== null ? this.salesOrderSearchService.get(id) : of([])
        ),
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe((details: SalesOrderDetail[]) =>
        this.salesOrderDetails.set(details)
      );
  }
}
