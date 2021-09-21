import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SalesOrderComponent } from './sales-order.component';
import { SearchComponent } from './search/search.component';
import { DetailComponent } from './detail/detail.component';



@NgModule({
  declarations: [SalesOrderComponent, SearchComponent, DetailComponent],
  imports: [
    CommonModule
  ]
})
export class SalesOrderModule { }
