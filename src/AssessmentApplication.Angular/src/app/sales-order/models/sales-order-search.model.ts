import { SortDirection } from "@shared/models";
import { PagedRequestModel } from "@shared/models/paged-request.model";

export class SalesOrderSearchModel extends PagedRequestModel {
  public orderDateStart: Date;
  public orderDateEnd: Date;
  public dueDateStart: Date;
  public dueDateEnd: Date;
  public shipDateStart: Date;
  public shipDateEnd: Date;
  public customerName: string;
  [key: string]: Date | number | string | SortDirection | null | undefined;
}
