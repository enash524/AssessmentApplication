import { Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Subject, of } from "rxjs";
import { map, switchMap, takeUntil } from "rxjs/operators";
import { SalesOrderDetail } from "@app/sales-order/models";
import { SalesOrderSearchService } from "@app/sales-order";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { SalesOrderInfoComponent } from "../widgets/sales-order-info/sales-order-info.component";

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
export class DetailsComponent implements OnInit, OnDestroy {
  public salesOrderDetails: SalesOrderDetail[];
  private _destroyed$: Subject<void> = new Subject();

  constructor(
    private activatedRoute: ActivatedRoute,
    private salesOrderService: SalesOrderSearchService
  ) {}

  ngOnInit(): void {
    this.activatedRoute.paramMap
      .pipe(
        map((params) => {
          const param = params.get("id");
          return param ? +param : null;
        }),
        switchMap((id) => (id ? this.salesOrderService.get(id) : of([]))),
        takeUntil(this._destroyed$)
      )
      .subscribe((details) => (this.salesOrderDetails = details));
  }

  ngOnDestroy(): void {
    this._destroyed$.next();
    this._destroyed$.complete();
  }
}
