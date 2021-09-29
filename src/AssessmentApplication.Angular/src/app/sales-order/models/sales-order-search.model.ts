import { PersonModel } from '.';

export class SalesOrderSearchModel {
    public orderDateStart: Date | null = null;
    public orderDateEnd: Date | null = null;
    public dueDateStart: Date | null = null;
    public dueDateEnd: Date | null = null;
    public shipDateStart: Date | null = null;
    public shipDateEnd: Date | null = null;
    public customerName: PersonModel | null = null;
}
