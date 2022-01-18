import { DateRangeModel } from '@shared/date-range';

export interface SearchModel {
  orderDate: DateRangeModel;
  dueDate: DateRangeModel;
  shipDate: DateRangeModel;
  customerName: string;
}
