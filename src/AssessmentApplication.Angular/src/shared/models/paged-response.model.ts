import { SortDirection } from '.';

export class PagedResponseModel<T> {
    public data: T | null = null;
    public limit: number = 10;
    public offset: number = 0;
    public recordCount: number = 0;
    public sortBy: string | null = null;
    public sortDirection: SortDirection = SortDirection.Asc;
}