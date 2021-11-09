import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SearchComponent } from './search/search.component';
import { DetailsComponent } from './details/details.component';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { InputTextModule } from 'primeng/inputtext';
import { ReactiveFormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { DateRangeComponent } from './widgets/date-range/date-range.component';
import { InputTextboxComponent } from './widgets/input-textbox/input-textbox.component';
import { SalesOrderInfoComponent } from './widgets/sales-order-info/sales-order-info.component';
import { SharedModule } from '@shared/shared.module';


@NgModule({
  declarations: [
    SearchComponent,
    DetailsComponent,
    DateRangeComponent,
    InputTextboxComponent,
    SalesOrderInfoComponent,
  ],
  imports: [
    ButtonModule,
    CommonModule,
    RouterModule,
    CalendarModule,
    InputTextModule,
    ReactiveFormsModule,
    TableModule,
    SharedModule,
  ]
})
export class SalesOrderModule {}
