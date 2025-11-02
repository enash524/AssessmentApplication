import { CommonModule } from "@angular/common";
import { Component, DestroyRef, inject, model, signal } from "@angular/core";
import { takeUntilDestroyed } from "@angular/core/rxjs-interop";
import {
  AbstractControl,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from "@angular/forms";
import { RouterModule } from "@angular/router";
import { SalesOrderSearchService } from "@app/sales-order";
import {
  SalesOrderHeaderModel,
  SalesOrderSearchModel,
} from "@app/sales-order/models";
import { DateRangeComponent } from "@shared/date-range/date-range.component";
import { InputTextboxComponent } from "@shared/input-textbox/input-textbox.component";
import {
  ColumnModel,
  DateRangeModel,
  PagedResponseModel,
  SortDirection,
} from "@shared/models";
import { FullAddressPipe } from "@shared/pipes/full-address.pipe";
import { ButtonModule } from "primeng/button";
import { TableModule } from "primeng/table";

@Component({
  selector: "app-search",
  templateUrl: "./search.component.html",
  styleUrl: "./search.component.scss",
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
  private salesOrderSearchService = inject(SalesOrderSearchService);
  public salesOrderHeader = model<SalesOrderHeaderModel[] | null>(null);
  public totalRecords = model<number>(0);
  public searchForm: FormGroup = new FormGroup<SearchForm>({
    orderDate: new FormControl<DateRangeModel | null>(new DateRangeModel()),
    dueDate: new FormControl<DateRangeModel | null>(new DateRangeModel()),
    shipDate: new FormControl<DateRangeModel | null>(new DateRangeModel()),
    customerName: new FormControl<string | null>(null),
  });

  private previousSearchModel = signal<SalesOrderSearchModel>(
    new SalesOrderSearchModel()
  );
  private salesOrderSearchModel = signal<PagedResponseModel<
    SalesOrderHeaderModel[]
  > | null>(null);

  public onPage(event: any) {
    this.previousSearchModel().offset = event.first;
    this.previousSearchModel().limit = event.rows;
    this.search(this.previousSearchModel());
  }

  public onReset() {
    this.salesOrderHeader.set(null);
  }

  public onSort(event: any) {
    this.previousSearchModel().sortBy = event.field;
    this.previousSearchModel().sortDirection =
      event.order === 1 ? SortDirection.Asc : SortDirection.Desc;
    this.search(this.previousSearchModel());
  }

  public onSubmit() {
    if (!this.searchForm.valid) {
      return;
    }

    this.previousSearchModel.set(this.getSearchModel());
    this.search(this.previousSearchModel());
  }

  private customerNameControl(): AbstractControl<string | null> | null {
    return this.searchForm.get("customerName");
  }

  private dueDateControl(): AbstractControl<DateRangeModel | null> | null {
    return this.searchForm.get("dueDate");
  }

  private orderDateControl(): AbstractControl<DateRangeModel | null> | null {
    return this.searchForm.get("orderDate");
  }

  private shipDateControl(): AbstractControl<DateRangeModel | null> | null {
    return this.searchForm.get("shipDate");
  }

  private getSearchModel() {
    const searchModel: SalesOrderSearchModel = new SalesOrderSearchModel();

    searchModel.customerName = this.customerNameControl()?.value;
    searchModel.dueDateEnd = this.dueDateControl()?.value?.toDate;
    searchModel.dueDateStart = this.dueDateControl()?.value?.fromDate;
    searchModel.orderDateEnd = this.orderDateControl()?.value?.toDate;
    searchModel.orderDateStart = this.orderDateControl()?.value?.fromDate;
    searchModel.shipDateEnd = this.shipDateControl()?.value?.toDate;
    searchModel.shipDateStart = this.shipDateControl()?.value?.fromDate;

    if (this.salesOrderSearchModel()) {
      searchModel.limit = this.salesOrderSearchModel().limit;
      searchModel.offset = this.salesOrderSearchModel().offset;
      searchModel.sortBy = this.salesOrderSearchModel().sortBy;
      searchModel.sortDirection = this.salesOrderSearchModel().sortDirection;
    }

    return searchModel;
  }

  private search(searchModel: SalesOrderSearchModel) {
    this.salesOrderSearchService
      .search(searchModel)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((result: PagedResponseModel<SalesOrderHeaderModel[]>) => {
        this.salesOrderSearchModel.set(result);
        this.salesOrderHeader.set(result.data);
        this.totalRecords.set(result.recordCount);
      });
  }
}

export type SearchForm = {
  orderDate: FormControl<DateRangeModel | null>;
  dueDate: FormControl<DateRangeModel | null>;
  shipDate: FormControl<DateRangeModel | null>;
  customerName: FormControl<string | null>;
};
