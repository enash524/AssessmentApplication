import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DetailsComponent } from './details/details.component';
import { SalesOrderComponent } from './sales-order.component';
import { SalesOrderModule } from './sales-order.module';
import { SearchComponent } from './search/search.component';

const routes: Routes = [
  {
    path: '',
    component: SalesOrderComponent,
    children: [
      {
        path: '',
        redirectTo: 'search'
      },
      {
        path: 'search',
        component: SearchComponent
      },
      {
        path: 'detail/:id',
        component: DetailsComponent
      },
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
    SalesOrderModule,
  ],
  exports: [
    RouterModule,
  ],
})
export class SalesOrderRouting {}
