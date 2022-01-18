import { SortDirection } from ".";

export abstract class PagedRequestModel {
    public limit: number = 10;
    public offset: number = 0;
    public sortBy: string;
    public sortDirection: SortDirection = SortDirection.Asc;
}
