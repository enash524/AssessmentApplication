import { CommonModule } from "@angular/common";
import { Component, Input } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { SalesOrderDetail } from "@app/sales-order/models";

@Component({
  selector: "app-sales-order-info",
  templateUrl: "./sales-order-info.component.html",
  styleUrls: ["./sales-order-info.component.scss"],
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
})
export class SalesOrderInfoComponent {
  @Input()
  public salesOrderDetail: SalesOrderDetail;
}
