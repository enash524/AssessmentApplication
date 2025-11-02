import { CommonModule } from "@angular/common";
import { Component, input } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { SalesOrderDetail } from "@app/sales-order/models";

@Component({
  selector: "app-sales-order-info",
  templateUrl: "./sales-order-info.component.html",
  styleUrl: "./sales-order-info.component.scss",
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
})
export class SalesOrderInfoComponent {
  public salesOrderDetail = input.required<SalesOrderDetail>();
}
