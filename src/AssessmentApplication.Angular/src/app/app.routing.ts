import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './home-page/home-page.component';
import { ForbiddenPageComponent } from './static-pages/forbidden-page/forbidden-page.component';
import { NotFoundPageComponent } from './static-pages/not-found-page/not-found-page.component';
import { UnavailablePageComponent } from './static-pages/unavailable-page/unavailable-page.component';

const routes: Routes = [
  {
    path: 'forbidden',
    pathMatch: 'full',
    component: ForbiddenPageComponent
  },
  {
    path: 'not-found',
    pathMatch: 'full',
    component: NotFoundPageComponent
  },
  {
    path: 'unavailable',
    pathMatch: 'full',
    component: UnavailablePageComponent
  },
  {
    path: 'sales-order',
    loadChildren: () => import('./sales-order/sales-order.routing').then(m => m.SalesOrderRouting)
  },
  {
    path: '',
    pathMatch: 'full',
    component: HomePageComponent
  },
  {
    path: '**',
    redirectTo: 'not-found'
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ],
})
export class AppRoutingModule { }
