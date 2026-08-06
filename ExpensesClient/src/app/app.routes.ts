import { Routes } from '@angular/router';
import { Login } from './login/login';
import { TransactionList } from './transaction-list/transaction-list';
import { Signup } from './signup/signup';
import { TransactionForm } from './transaction-form/transaction-form';

export const routes: Routes = [
  {
    path: 'login',
    component: Login,
  },
  {
    path: 'signup',
    component: Signup,
  },
  {
    path: 'transactions',
    component: TransactionList
  },
  {
    path: 'add',
    component: TransactionForm
  },
  {
    path: 'edit/:id',
    component: TransactionForm
  },
  {
    path:'**',
    redirectTo: '/transactions'
  },
  {
    path:'',
    redirectTo: '/transactions',
    pathMatch: 'full'
  }
];
