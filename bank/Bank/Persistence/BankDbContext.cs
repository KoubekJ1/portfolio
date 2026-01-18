using System.Data.Common;
using Bank.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bank.Persistence;

public class BankDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
}