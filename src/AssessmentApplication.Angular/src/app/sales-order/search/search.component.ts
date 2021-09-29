import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ColumnModel } from '@shared/models';
import { Subscription } from 'rxjs';
import { SalesOrderHeaderModel } from '..';
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
            field: '',
            header: 'Customer Name'
        },
        {
            field: '',
            header: 'Account Number'
        },
        {
            field: '',
            header: 'Ship To Address'
        },
        {
            field: '',
            header: 'Ship Method'
        },
        {
            field: '',
            header: 'Sub Total'
        },
        {
            field: '',
            header: 'Tax'
        },
        {
            field: '',
            header: 'Freight'
        },
        {
            field: '',
            header: 'Total'
        }
    ];

    public salesOrderHeader: SalesOrderHeaderModel[] | null = null;
    public onChange: Function = () => { };
    public onTouched: Function = () => { };
    public searchForm: FormGroup;
    private _subscriptions: Subscription[] = [];

    constructor(private formBuilder: FormBuilder) {
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

    public onReset() {
        this.salesOrderHeader = null;
        this.searchForm.reset();
    }

    public onSubmit() {
        this.salesOrderHeader = [];
    }
}
