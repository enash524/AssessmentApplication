import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SearchComponent } from './search/search.component';
import { DetailsComponent } from './details/details.component';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { SalesOrderInfoComponent } from './widgets/sales-order-info/sales-order-info.component';
import { SharedModule } from '@shared/shared.module';


@NgModule({
  declarations: [
    SearchComponent,
    DetailsComponent,
    SalesOrderInfoComponent,
  ],
  imports: [
    ButtonModule,
    CommonModule,
    RouterModule,
    CalendarModule,
    FormsModule,
    InputTextModule,
    ReactiveFormsModule,
    SharedModule,
    TableModule,
  ]
})
export class SalesOrderModule { }
