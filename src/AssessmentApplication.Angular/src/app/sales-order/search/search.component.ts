import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { ColumnModel } from 'src/shared';
import { SalesOrderHeaderModel } from '..';
import { DateRangeValidator } from 'src/shared/validators/date-range.validator';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {

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

  public searchForm: FormGroup = this.formBuilder.group({
    orderDate: this.formBuilder.group({
      orderDateStart: null,
      orderDateEnd: null
    },
    {
      validators: DateRangeValidator('orderDateStart', 'orderDateEnd')
    }),
    dueDate: this.formBuilder.group({
      dueDateStart: null,
      dueDateEnd: null
    },
    {
      validators: DateRangeValidator('dueDateStart', 'dueDateEnd')
    }),
    shipDate: this.formBuilder.group({
      shipDateStart: null,
      shipDateEnd: null
    },
    {
      validators: DateRangeValidator('shipDateStart', 'shipDateEnd')
    }),
    customerName: null
  });

  constructor(
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {

  }

  public onReset() {
    this.searchForm.reset();
    // TODO - FOR TESTING PURPOSES ONLY!!!
    this.salesOrderHeader = null;
  }

  public onSubmit() {
    console.log(this.searchForm.value);

    if (this.searchForm.valid) {
      alert('form is valid');
      this.salesOrderHeader = [];
    } else {
      alert('form is invalid');
    }
  }

}
