import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ColumnModel, PagedResponseModel } from '@shared/models';
import { Subscription } from 'rxjs';
import { SalesOrderHeaderModel, SalesOrderSearchModel, SalesOrderSearchService } from '..';
import { DateRangeFormValues } from '../widgets/date-range/date-range.component';

export interface SearchFormValues {
    orderDate: DateRangeFormValues,
    dueDate: DateRangeFormValues,
    shipDate: DateRangeFormValues,
    customerName: string
}

@Component({
    selector: 'app-search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.scss'],
})
export class SearchComponent implements OnDestroy {

    public columns: ColumnModel[] = [
        {
            field: 'fullName',
            header: 'Customer Name'
        },
        {
            field: 'accountNumber',
            header: 'Account Number'
        },
        {
            field: 'address',
            header: 'Ship To Address'
        },
        {
            field: 'shipMethodName',
            header: 'Ship Method'
        },
        {
            field: 'subTotal',
            header: 'Sub Total'
        },
        {
            field: 'taxAmt',
            header: 'Tax'
        },
        {
            field: 'freight',
            header: 'Freight'
        },
        {
            field: 'totalDue',
            header: 'Total'
        }
    ];

    public salesOrderHeader: SalesOrderHeaderModel[] | null = null;
    public onChange: Function = () => { };
    public onTouched: Function = () => { };
    public searchForm: FormGroup;
    public totalRecords: number = 0;

    private _salesOrderSearchModel: PagedResponseModel<SalesOrderHeaderModel[]> | null = null;
    private _subscriptions: Subscription[] = [];

    constructor(
        private formBuilder: FormBuilder,
        private salesOrderSearch: SalesOrderSearchService
    ) {
        this.searchForm = this.formBuilder.group({
            orderDate: [],
            dueDate: [],
            shipDate: [],
            customerName: []
        });

        this._subscriptions.push(
            this.searchForm.valueChanges.subscribe(value => {
                this.onChange(value);
                this.onTouched();
            })
        );
    }

    public ngOnDestroy() {
        this._subscriptions.forEach((s: Subscription) => s.unsubscribe());
    }

    public onPage(event: any) {
        // TODO - IMPLEMENT ME!!!
        // event.first - record number to start at - record number / rows = current page - 0 based
        // event.rows - number of rows per page
        console.log('onPage', event);
    }

    public onReset() {
        this.salesOrderHeader = null;
        this.searchForm.reset();
    }

    public onSort(event: any) {
        // TODO - IMPLEMENT ME!!!
        // event.field - from columns value
        // event.order - 1 == asc, -1 == desc
        console.log('onSort', event);
    }

    public onSubmit() {
        if (this.searchForm.invalid) {
            return;
        }

        const searchModel: SalesOrderSearchModel = this.getSearchModel();
        this.salesOrderSearch.search(searchModel).subscribe({
            next: (result) => {
                console.log('next', result)
                this._salesOrderSearchModel = result;
                this.salesOrderHeader = result.data;
                this.totalRecords = result.recordCount
            },
            error: (error) => {
                console.log('error', error)
            },
            complete: () => {
                console.log('complete')
            }
        });
    }

    private getSearchModel() {
        const searchModel: SalesOrderSearchModel = new SalesOrderSearchModel();

        searchModel.customerName = this.searchForm.controls.customerName.value?.textboxValue;
        searchModel.dueDateEnd = this.searchForm.controls.dueDate.value?.toDate;
        searchModel.dueDateStart = this.searchForm.controls.dueDate.value?.fromDate;
        searchModel.orderDateEnd = this.searchForm.controls.orderDate.value?.toDate;
        searchModel.orderDateStart = this.searchForm.controls.orderDate.value?.fromDate;
        searchModel.shipDateEnd = this.searchForm.controls.shipDate.value?.toDate;
        searchModel.shipDateStart = this.searchForm.controls.shipDate.value?.fromDate;

        if (this._salesOrderSearchModel) {
            searchModel.limit = this._salesOrderSearchModel.limit;
            searchModel.offset = this._salesOrderSearchModel.offset;
            searchModel.sortBy = this._salesOrderSearchModel.sortBy;
            searchModel.sortDirection = this._salesOrderSearchModel.sortDirection;
        }

        return searchModel;
    }
}
