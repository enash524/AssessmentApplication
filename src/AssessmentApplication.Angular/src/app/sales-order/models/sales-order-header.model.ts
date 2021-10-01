import { AddressModel } from '.';
import { PersonModel } from '.';
import { ShipMethodModel } from '.';

export interface SalesOrderHeaderModel {
    salesOrderId: number | null;
    accountNumber: string | null;
    freight: number | null;
    subTotal: number | null;
    taxAmt: number | null;
    totalDue: number | null;
    person: PersonModel | null;
    shipMethod: ShipMethodModel | null;
    shipToAddress: AddressModel | null;
}
