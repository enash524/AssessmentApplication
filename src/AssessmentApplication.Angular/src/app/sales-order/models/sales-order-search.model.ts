import { SortDirection } from '@shared/models';
import { PagedRequestModel } from '@shared/models/paged-request.model';

export class SalesOrderSearchModel extends PagedRequestModel {
    public orderDateStart: Date | null = null;
    public orderDateEnd: Date | null = null;
    public dueDateStart: Date | null = null;
    public dueDateEnd: Date | null = null;
    public shipDateStart: Date | null = null;
    public shipDateEnd: Date | null = null;
    public customerName: string | null = null;
    [key: string]: Date | number | string | SortDirection | null | undefined
}
