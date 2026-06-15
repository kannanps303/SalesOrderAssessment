import { Routes } from '@angular/router';
import { SalesOrderComponent } from './features/sales-order/pages/sales-order/sales-order.component';
import { LoginComponent } from './features/login/login/login.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'sales-order',
    component: SalesOrderComponent
  },
  {
    path: 'login',
    component: LoginComponent
  }
];
