import { Component, OnDestroy } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import {
  SalesOrderHeaderModel,
  SalesOrderSearchModel,
} from "@app/sales-order/models";
import {
  ColumnModel,
  DateRangeModel,
  PagedResponseModel,
  SortDirection,
} from "@shared/models";
import { SalesOrderSearchService } from "@app/sales-order";
import { Subject, takeUntil } from "rxjs";

@Component({
  selector: "app-search",
  templateUrl: "./search.component.html",
  styleUrls: ["./search.component.scss"],
})
export class SearchComponent implements OnDestroy {
  public columns: ColumnModel[] = [
    {
      field: "fullName",
      header: "Customer Name",
    },
    {
      field: "accountNumber",
      header: "Account Number",
    },
    {
      field: "address",
      header: "Ship To Address",
    },
    {
      field: "shipMethodName",
      header: "Ship Method",
    },
    {
      field: "subTotal",
      header: "Sub Total",
    },
    {
      field: "taxAmt",
      header: "Tax",
    },
    {
      field: "freight",
      header: "Freight",
    },
    {
      field: "totalDue",
      header: "Total",
    },
  ];

  private _destroyed$: Subject<void> = new Subject();
  public salesOrderHeader: SalesOrderHeaderModel[];
  public totalRecords: number = 0;
  public searchForm: FormGroup = new FormGroup<SearchForm>({
    orderDate: new FormControl<DateRangeModel | null>(new DateRangeModel()),
    dueDate: new FormControl<DateRangeModel | null>(new DateRangeModel()),
    shipDate: new FormControl<DateRangeModel | null>(new DateRangeModel()),
    customerName: new FormControl<string>(""),
  });

  private _previousSearchModel: SalesOrderSearchModel =
    new SalesOrderSearchModel();
  private _salesOrderSearchModel: PagedResponseModel<SalesOrderHeaderModel[]>;

  constructor(private salesOrderSearch: SalesOrderSearchService) {}

  public ngOnDestroy() {
    this._destroyed$.next();
    this._destroyed$.complete();
  }

  public onPage(event: any) {
    this._previousSearchModel.offset = event.first;
    this._previousSearchModel.limit = event.rows;
    this.search(this._previousSearchModel);
  }

  public onReset() {
    this.salesOrderHeader = null;
  }

  public onSort(event: any) {
    this._previousSearchModel.sortBy = event.field;
    this._previousSearchModel.sortDirection =
      event.order === 1 ? SortDirection.Asc : SortDirection.Desc;
    this.search(this._previousSearchModel);
  }

  public onSubmit() {
    if (this.searchForm.invalid) {
      return;
    }

    this._previousSearchModel = this.getSearchModel();
    this.search(this._previousSearchModel);
  }

  private getSearchModel() {
    const searchModel: SalesOrderSearchModel = new SalesOrderSearchModel();

    searchModel.customerName = this.searchForm.controls["customerName"].value;
    searchModel.dueDateEnd = this.searchForm.controls["dueDate"].value?.toDate;
    searchModel.dueDateStart =
      this.searchForm.controls["dueDate"].value?.fromDate;
    searchModel.orderDateEnd =
      this.searchForm.controls["orderDate"].value?.toDate;
    searchModel.orderDateStart =
      this.searchForm.controls["orderDate"].value?.fromDate;
    searchModel.shipDateEnd =
      this.searchForm.controls["shipDate"].value?.toDate;
    searchModel.shipDateStart =
      this.searchForm.controls["shipDate"].value?.fromDate;

    if (this._salesOrderSearchModel) {
      searchModel.limit = this._salesOrderSearchModel.limit;
      searchModel.offset = this._salesOrderSearchModel.offset;
      searchModel.sortBy = this._salesOrderSearchModel.sortBy;
      searchModel.sortDirection = this._salesOrderSearchModel.sortDirection;
    }

    return searchModel;
  }

  private search(searchModel: SalesOrderSearchModel) {
    this.salesOrderSearch
      .search(searchModel)
      .pipe(takeUntil(this._destroyed$))
      .subscribe({
        next: (result) => {
          this._salesOrderSearchModel = result;
          this.salesOrderHeader = result.data;
          this.totalRecords = result.recordCount;
        },
        error: (error) => {
          console.log("error", error);
        },
      });
  }
}

export type SearchForm = {
  orderDate: FormControl<DateRangeModel | null>;
  dueDate: FormControl<DateRangeModel | null>;
  shipDate: FormControl<DateRangeModel | null>;
  customerName: FormControl<string>;
};
