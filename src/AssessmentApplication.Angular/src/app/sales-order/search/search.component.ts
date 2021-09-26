import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { ColumnModel } from 'src/shared';
import { SalesOrderHeaderModel } from '..';

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

  public salesOrderHeader: SalesOrderHeaderModel[] = [];

  public searchForm: FormGroup = this.formBuilder.group({
    orderDateStart: null,
    orderDateEnd: null,
    dueDateStart: null,
    dueDateEnd: null,
    shipDateStart: null,
    shipDateEnd: null,
    customerName: null
  });

  constructor(
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {

  }

  public onReset() {
    this.searchForm.reset();
  }

  public onSubmit() {
    console.log(this.searchForm.value);
  }

}
