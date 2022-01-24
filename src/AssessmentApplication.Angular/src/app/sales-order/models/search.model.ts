import { DateRangeModel } from '@shared/models';

export interface SearchModel {
  orderDate: DateRangeModel;
  dueDate: DateRangeModel;
  shipDate: DateRangeModel;
  customerName: string;
}
