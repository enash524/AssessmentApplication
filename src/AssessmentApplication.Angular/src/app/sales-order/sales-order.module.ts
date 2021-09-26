import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchComponent } from './search/search.component';
import { DetailsComponent } from './details/details.component';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { InputTextModule } from 'primeng/inputtext';
import { ReactiveFormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';


@NgModule({
  declarations: [
    SearchComponent,
    DetailsComponent,
  ],
  imports: [
    ButtonModule,
    CommonModule,
    CalendarModule,
    InputTextModule,
    ReactiveFormsModule,
    TableModule,
  ]
})
export class SalesOrderModule { }
