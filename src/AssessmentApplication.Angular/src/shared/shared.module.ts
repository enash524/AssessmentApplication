import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DateRangeComponent } from './date-range/date-range.component';
import { FullAddressPipe } from './pipes/full-address.pipe';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { InputTextboxComponent } from './input-textbox/input-textbox.component';


@NgModule({
  declarations: [
    DateRangeComponent,
    FullAddressPipe,
    InputTextboxComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ButtonModule,
    CalendarModule,
  ],
  exports: [
    DateRangeComponent,
    FullAddressPipe,
    InputTextboxComponent,
  ]
})
export class SharedModule { }
