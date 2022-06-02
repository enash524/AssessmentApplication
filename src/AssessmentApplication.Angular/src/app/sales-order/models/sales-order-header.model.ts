import { AddressModel } from ".";
import { PersonModel } from ".";
import { ShipMethodModel } from ".";

export interface SalesOrderHeaderModel {
  salesOrderId: number;
  accountNumber: string;
  freight: number;
  subTotal: number;
  taxAmt: number;
  totalDue: number;
  person: PersonModel;
  shipMethod: ShipMethodModel;
  shipToAddress: AddressModel;
}
