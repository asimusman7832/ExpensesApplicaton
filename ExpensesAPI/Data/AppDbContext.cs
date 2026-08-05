using ExpensesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace ExpensesAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
