import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchComponent } from './search/search.component';
import { DetailsComponent } from './details/details.component';



@NgModule({
  declarations: [
    SearchComponent,
    DetailsComponent
  ],
  imports: [
    CommonModule
  ]
})
export class SalesOrderModule { }
