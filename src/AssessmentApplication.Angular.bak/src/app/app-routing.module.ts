import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SalesOrderComponent } from './sales-order/sales-order.component';

const routes: Routes = [
  { path: 'sales-order', pathMatch: 'full', component: SalesOrderComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
