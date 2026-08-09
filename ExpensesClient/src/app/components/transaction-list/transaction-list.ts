import { Component } from '@angular/core';
import { Transaction } from '../../models/transaction';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-transaction-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './transaction-list.html',
  styleUrl: './transaction-list.css',
})
export class TransactionList {
  transactions: Transaction[] = [
    {
      id: 1,
      type: 'Expense',
      category: 'Food',
      amount: 5000,
      createdAt: new Date(),
      updatedAt: new Date(),
    },
    {
      id: 1,
      type: 'Expense',
      category: 'Food',
      amount: 5000,
      createdAt: new Date(),
      updatedAt: new Date(),
    }
  ];
  date: Date = new Date();
}
