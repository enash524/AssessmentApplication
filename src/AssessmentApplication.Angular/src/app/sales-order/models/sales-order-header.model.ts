import { AddressModel } from '.';
import { PersonModel } from '.';
import { ShipMethodModel } from '.';

export class SalesOrderHeaderModel {
    public salesOrderId: number | null = null;
    public accountNumber: string | null = null;
    public freight: number | null = null;
    public subTotal: number | null = null;
    public taxAmt: number | null = null;
    public totalDue: number | null = null;
    public person: PersonModel | null = null;
    public shipMethod: ShipMethodModel | null = null;
    public shipToAddress: AddressModel | null = null;
}
