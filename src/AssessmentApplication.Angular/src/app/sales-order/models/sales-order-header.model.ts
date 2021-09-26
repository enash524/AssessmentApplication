import { AddressModel } from '.';
import { PersonModel } from '.';
import { ShipMethodModel } from '.';

export class SalesOrderHeaderModel {
    public salesOrderId: number | null;
    public accountNumber: string | null;
    public freight: number | null;
    public subTotal: number | null;
    public taxAmt: number | null;
    public totalDue: number | null;
    public person: PersonModel | null;
    public shipMethod: ShipMethodModel | null;
    public shipToAddress: AddressModel | null;

    constructor() {
        this.salesOrderId = null;
        this.accountNumber = null;
        this.freight = null;
        this.subTotal = null;
        this.taxAmt = null;
        this.totalDue = null;
        this.person = null;
        this.shipMethod = null;
        this.shipToAddress = null;
    }
}
