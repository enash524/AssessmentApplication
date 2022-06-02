import { Component, Input } from "@angular/core";
import { SalesOrderDetail } from "@app/sales-order/models";

@Component({
  selector: "app-sales-order-info",
  templateUrl: "./sales-order-info.component.html",
  styleUrls: ["./sales-order-info.component.scss"],
})
export class SalesOrderInfoComponent {
  @Input()
  public salesOrderDetail: SalesOrderDetail;
}
