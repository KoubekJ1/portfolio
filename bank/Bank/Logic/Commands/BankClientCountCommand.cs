using System.Numerics;
using Bank.Persistence;
using Bank.Server;

namespace Bank.Logic.Commands;

public class BankClientCountCommand : ICommand
{
    public BankClientCountCommand()
    {
        
    }

    public string Execute(ConnectionContext context)
    {
        var dbContext = new BankDbContext();

        int total = dbContext.Accounts.Count();

        return total.ToString();
    }
}