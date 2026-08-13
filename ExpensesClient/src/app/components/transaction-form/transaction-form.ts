import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TransactionService } from '../../services/transaction.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-transaction-form',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './transaction-form.html',
  styleUrl: './transaction-form.css',
})
export class TransactionForm implements OnInit {
  transactionForm: FormGroup;

  incomeCategories = ['Salary', 'Business', 'Investments', 'Gifts', 'Other'];
  expenseCategories = ['Food', 'Transportation', 'Utilities', 'Entertainment'];
  availableCategories: string[] = [];

  editMode = false;
  transId?: number;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private transactionService: TransactionService,
    private activatedRoute: ActivatedRoute,
  ) {
    this.transactionForm = this.fb.group({
      type: ['Expense', Validators.required],
      category: ['', Validators.required],
      amount: ['', [Validators.required, Validators.min(0)]],
    });
  }

  ngOnInit(): void {
    const type = this.transactionForm.get('type')?.value;
    this.updateAvailableCat(type);

    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id) {
      this.editMode = true;
      this.transId = +id;

      this.loadTransaction(this.transId);
    }
  }
  onSubmit() {
    if (this.transactionForm.valid) {
      const transactionData = this.transactionForm.value;

      if (this.editMode && this.transId !== undefined) {
        this.transactionService.update(this.transId, transactionData).subscribe({
          next: () => {
            //this.router.navigate(['/transactions']);
            Swal.fire({
              title: 'Updated!',
              text: 'Transaction updated successfully.',
              icon: 'success',
              timer: 1500,
              showConfirmButton: false,
            }).then(() => {
              this.router.navigate(['/transactions']);
            });
          },
          error: (error) => {
            console.log('Error updating transaction:', error);
            Swal.fire({
              title: 'Error!',
              text: 'Unable to save transaction.',
              icon: 'error',
              confirmButtonText: 'OK',
            });
          },
        });
      } else {
        console.log('Transaction Data:', transactionData);
        this.transactionService.create(transactionData).subscribe({
          next: () => {
            //this.router.navigate(['/transactions']);
            Swal.fire({
              title: 'Transaction Created!',
              text: 'Transaction created successfully.',
              icon: 'success',
              timer: 1500,
              showConfirmButton: false,
            }).then(() => {
              this.router.navigate(['/transactions']);
            });
          },
          error: (error) => {
            console.log('Error updating transaction:', error);
            Swal.fire({
              title: 'Error!',
              text: 'Unable to save transaction.',
              icon: 'error',
              confirmButtonText: 'OK',
            });
          },
        });
      }
    }
  }
  onTypeChange() {
    const type = this.transactionForm.get('type')?.value;
    this.updateAvailableCat(type);
  }
  updateAvailableCat(type: string) {
    this.availableCategories = type === 'Expense' ? this.expenseCategories : this.incomeCategories;
    this.transactionForm.patchValue({ category: '' });
  }

  cancel() {
    this.router.navigate(['/transactions']);
  }
  loadTransaction(id: number): void {
    this.transactionService.getById(id).subscribe({
      next: (transaction) => {
        console.log('Loaded transaction:', transaction);

        this.updateAvailableCat(transaction.type);

        this.transactionForm.patchValue({
          type: transaction.type,
          category: transaction.category,
          amount: transaction.amount,
        });
      },
      error: (error) => {
        console.log('Error loading transaction:', error);
      },
    });
  }
}
