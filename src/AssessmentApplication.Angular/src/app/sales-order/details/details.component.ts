import { Component, inject, input } from "@angular/core";
import { toObservable, toSignal } from "@angular/core/rxjs-interop";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { SalesOrderSearchService } from "@app/sales-order";
import { of } from "rxjs";
import { catchError, switchMap } from "rxjs/operators";
import { SalesOrderInfoComponent } from "../widgets/sales-order-info/sales-order-info.component";

@Component({
  selector: "app-details",
  templateUrl: "./details.component.html",
  styleUrl: "./details.component.scss",
  imports: [FormsModule, ReactiveFormsModule, SalesOrderInfoComponent],
})
export class DetailsComponent {
  public id = input.required<number>();
  private salesOrderSearchService = inject(SalesOrderSearchService);
  public salesOrderDetails = toSignal(
    toObservable(this.id).pipe(
      switchMap((id) => this.salesOrderSearchService.get(id)),
      catchError(() => of([]))
    )
  );
}
