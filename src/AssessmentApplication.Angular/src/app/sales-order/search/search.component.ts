import { Component, DestroyRef, inject, model, signal } from "@angular/core";
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from "@angular/forms";
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
import { CommonModule } from "@angular/common";
import { FullAddressPipe } from "@shared/pipes/full-address.pipe";
import { TableModule } from "primeng/table";
import { RouterModule } from "@angular/router";
import { ButtonModule } from "primeng/button";
import { DateRangeComponent } from "@shared/date-range/date-range.component";
import { InputTextboxComponent } from "@shared/input-textbox/input-textbox.component";
import { takeUntilDestroyed } from "@angular/core/rxjs-interop";

@Component({
  selector: "app-search",
  templateUrl: "./search.component.html",
  styleUrls: ["./search.component.scss"],
  standalone: true,
  imports: [
    ButtonModule,
    CommonModule,
    DateRangeComponent,
    FormsModule,
    FullAddressPipe,
    InputTextboxComponent,
    ReactiveFormsModule,
    RouterModule,
    TableModule,
  ],
})
export class SearchComponent {
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

  private destroyRef = inject(DestroyRef);
  public salesOrderHeader = model<SalesOrderHeaderModel[]>(null);
  public totalRecords = model<number>(0);
  public searchForm: FormGroup = new FormGroup<SearchForm>({
    orderDate: new FormControl<DateRangeModel | null>(new DateRangeModel()),
    dueDate: new FormControl<DateRangeModel | null>(new DateRangeModel()),
    shipDate: new FormControl<DateRangeModel | null>(new DateRangeModel()),
    customerName: new FormControl<string | null>(null),
  });

  private _previousSearchModel = signal<SalesOrderSearchModel>(
    new SalesOrderSearchModel()
  );
  private _salesOrderSearchModel =
    signal<PagedResponseModel<SalesOrderHeaderModel[]>>(null);

  constructor(private salesOrderSearch: SalesOrderSearchService) {}

  public onPage(event: any) {
    this._previousSearchModel().offset = event.first;
    this._previousSearchModel().limit = event.rows;
    this.search(this._previousSearchModel());
  }

  public onReset() {
    this.salesOrderHeader = null;
  }

  public onSort(event: any) {
    this._previousSearchModel().sortBy = event.field;
    this._previousSearchModel().sortDirection =
      event.order === 1 ? SortDirection.Asc : SortDirection.Desc;
    this.search(this._previousSearchModel());
  }

  public onSubmit() {
    if (this.searchForm.invalid) {
      return;
    }

    this._previousSearchModel.set(this.getSearchModel());
    this.search(this._previousSearchModel());
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
      searchModel.limit = this._salesOrderSearchModel().limit;
      searchModel.offset = this._salesOrderSearchModel().offset;
      searchModel.sortBy = this._salesOrderSearchModel().sortBy;
      searchModel.sortDirection = this._salesOrderSearchModel().sortDirection;
    }

    return searchModel;
  }

  private search(searchModel: SalesOrderSearchModel) {
    this.salesOrderSearch
      .search(searchModel)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (result: PagedResponseModel<SalesOrderHeaderModel[]>) => {
          this._salesOrderSearchModel.set(result);
          this.salesOrderHeader.set(result.data);
          this.totalRecords.set(result.recordCount);
        },
      });
  }
}

export type SearchForm = {
  orderDate: FormControl<DateRangeModel | null>;
  dueDate: FormControl<DateRangeModel | null>;
  shipDate: FormControl<DateRangeModel | null>;
  customerName: FormControl<string | null>;
};
