import { SortDirection } from '.';

export class PagedResponse<T> {
    public currentPage: number = 1;
    public data: T | null = null;
    public pageSize: number = 10;
    public recordCount: number = 0;
    public sortBy: string | null = null;
    public sortDirection: SortDirection = SortDirection.ASC;
}