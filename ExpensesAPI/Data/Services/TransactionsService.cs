using ExpensesAPI.DTOs;
using ExpensesAPI.Models;

namespace ExpensesAPI.Data.Services
{
    public interface ITransactionsService
    {
        List<Transaction> GetAll();
        Transaction? GetById(int id);
        Transaction Add(PostTransactionDTO transaction);
        Transaction? Update(int id, PutTransactionDTO transaction);
        void Delete(int id);
    }
    public class TransactionsService(AppDbContext context) : ITransactionsService
    {
        public Transaction Add(PostTransactionDTO transaction)
        {
            var newTrans = new Transaction()
            {
                Amount = transaction.Amount,
                Type = transaction.Type,
                Category = transaction.Category,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            context.Transactions.Add(newTrans);
            context.SaveChanges();

            return newTrans;
        }

        public void Delete(int id)
        {
            var transDb = context.Transactions.FirstOrDefault(x => x.Id == id);

            if (transDb != null)
            {
                context.Transactions.Remove(transDb);

                context.SaveChanges();
            }
        }

        public List<Transaction> GetAll()
        {
            var allTrans = context.Transactions.ToList();

            return allTrans;
        }

        public Transaction? GetById(int id)
        {
            var transDB = context.Transactions.FirstOrDefault(x => x.Id == id);

            return transDB;
        }

        public Transaction? Update(int id, PutTransactionDTO transaction)
        {
            var transDb = context.Transactions.FirstOrDefault(x => x.Id == id);

            if (transDb != null)
            {
                transDb.Amount = transaction.Amount;
                transDb.Type = transaction.Type;
                transDb.Category = transaction.Category;
                transDb.UpdatedAt = DateTime.UtcNow;
                context.Transactions.Update(transDb);

                context.SaveChanges();
            }

            return transDb;
        }
    }
}
