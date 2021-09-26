import { PersonModel } from '.';

export class SalesOrderSearchModel {
    public orderDateStart: Date | null;
    public orderDateEnd: Date | null;
    public dueDateStart: Date | null;
    public dueDateEnd: Date | null;
    public shipDateStart: Date | null;
    public shipDateEnd: Date | null;
    public customerName: PersonModel | null;

    constructor() {
        this.orderDateStart = null;
        this.orderDateEnd = null;
        this.dueDateStart = null;
        this.dueDateEnd = null;
        this.shipDateStart = null;
        this.shipDateEnd = null;
        this.customerName = null;
    }
}