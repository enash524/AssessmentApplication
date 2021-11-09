import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FullAddressPipe } from './pipes/full-address.pipe';


@NgModule({
  declarations: [
    FullAddressPipe,
  ],
  imports: [
    CommonModule
  ],
  exports: [
    FullAddressPipe,
  ]
})
export class SharedModule {}
