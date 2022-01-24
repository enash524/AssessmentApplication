import { SortDirection } from '.';

export class PagedResponseModel<T> {
  public data: T;
  public limit: number = 10;
  public offset: number = 0;
  public recordCount: number = 0;
  public sortBy: string;
  public sortDirection: SortDirection = SortDirection.Asc;
}
