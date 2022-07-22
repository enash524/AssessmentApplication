import { Component, OnDestroy } from "@angular/core";
import { FormBuilder, FormControl, FormGroup } from "@angular/forms";
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
import { Subscription } from "rxjs";

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

  public salesOrderHeader: SalesOrderHeaderModel[];
  public onChange: Function = () => {};
  public onTouched: Function = () => {};
  public searchForm: FormGroup;
  public totalRecords: number = 0;

  private _previousSearchModel: SalesOrderSearchModel =
    new SalesOrderSearchModel();
  private _salesOrderSearchModel: PagedResponseModel<SalesOrderHeaderModel[]>;
  private _subscriptions: Subscription[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private salesOrderSearch: SalesOrderSearchService
  ) {
    this.searchForm = this.formBuilder.group({
      orderDate: new FormControl<DateRangeModel>({
        fromDate: null,
        toDate: null,
      }),
      dueDate: new FormControl<DateRangeModel>({
        fromDate: null,
        toDate: null,
      }),
      shipDate: new FormControl<DateRangeModel>({
        fromDate: null,
        toDate: null,
      }),
      customerName: new FormControl<string | null>(null),
    });

    this._subscriptions.push(
      this.searchForm.valueChanges.subscribe((value) => {
        this.onChange(value);
        this.onTouched();
      })
    );
  }

  public ngOnDestroy() {
    this._subscriptions.forEach((s: Subscription) => s.unsubscribe());
  }

  public onPage(event: any) {
    this._previousSearchModel.offset = event.first;
    this._previousSearchModel.limit = event.rows;
    this.search(this._previousSearchModel);
  }

  public onReset() {
    this.salesOrderHeader = null;
    this.searchForm.reset();
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

    searchModel.customerName =
      this.searchForm.controls["customerName"].value?.textboxValue;
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
    this.salesOrderSearch.search(searchModel).subscribe({
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
