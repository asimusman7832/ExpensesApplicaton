import { Component, OnInit } from '@angular/core';
import { Transaction } from '../../models/transaction';
import { CommonModule } from '@angular/common';
import { TransactionService } from '../../services/transaction.service';
import { ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-transaction-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './transaction-list.html',
  styleUrl: './transaction-list.css',
})
export class TransactionList implements OnInit {
  transactions: Transaction[] = [];
  constructor(
    private transactionService: TransactionService,
    private cdr: ChangeDetectorRef,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.loadTransactions();
  }

  loadTransactions(): void {
    this.transactionService.getAll().subscribe({
      next: (data) => {
        this.transactions = data;
        this.cdr.detectChanges();
      },
    });
  }

  getTotalIncome(): number {
    return this.transactions
      .filter((t) => t.type === 'Income')
      .reduce((sum, t) => sum + t.amount, 0);
  }

  getTotalExpenses(): number {
    return this.transactions
      .filter((t) => t.type === 'Expense')
      .reduce((sum, t) => sum + t.amount, 0);
  }

  getNetBalance(): number {
    return this.getTotalIncome() - this.getTotalExpenses();
  }
  editTransaction(transaction: Transaction): void {
    console.log('Editing transaction:', transaction);
    if (transaction.id) {
      this.router.navigate(['/edit/', transaction.id]);
    }
  }
  deleteTransaction(transaction: Transaction): void {
    if (!transaction.id) {
      return;
    }

    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to recover this transaction!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'Cancel',
      confirmButtonColor: '#d33',
      cancelButtonColor: '#6c757d',
    }).then((result) => {
      if (result.isConfirmed) {
        this.transactionService.delete(transaction.id).subscribe({
          next: () => {
            // Reload transactions
            this.loadTransactions();

            // Success popup
            Swal.fire({
              title: 'Deleted!',
              text: 'Transaction has been deleted successfully.',
              icon: 'success',
              timer: 1500,
              showConfirmButton: false,
            });
          },

          error: (error) => {
            console.error('Error deleting transaction:', error);

            Swal.fire({
              title: 'Error!',
              text: 'Unable to delete the transaction.',
              icon: 'error',
              confirmButtonText: 'OK',
            });
          },
        });
      }
    });
  }
}
