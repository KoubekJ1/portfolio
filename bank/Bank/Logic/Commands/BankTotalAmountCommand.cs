using System.Numerics;
using Bank.Persistence;
using Bank.Server;

namespace Bank.Logic.Commands;

public class BankTotalAmountCommand : ICommand
{
    public BankTotalAmountCommand()
    {
        
    }

    public string Execute(ConnectionContext context)
    {
        var dbContext = new BankDbContext();

        BigInteger total = 0;

        foreach (var num in dbContext.Accounts.Select(x => x.Balance))
        {
            total += num;
        }

        return total.ToString();
    }
}