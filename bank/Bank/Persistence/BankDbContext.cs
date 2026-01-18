using System.Configuration;
using System.Data.Common;
using Bank.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bank.Persistence;

public class BankDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }

    public BankDbContext()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        if (connectionString == null) throw new InvalidOperationException("ConnectionString doesn't have a value in config!");
        optionsBuilder
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .UseValidationCheckConstraints();
    }
}