using Bank.Persistence;
using Bank.Server;

namespace Bank.Logic.Commands;

public class RemoveAccountCommand : ICommand
{
    private int accountNumber;

    public RemoveAccountCommand(int accountNumber)
    {
        if (accountNumber < 10000 || accountNumber > 99999)
        {
            throw new ArgumentException("Invalid account number!");
        }
        this.accountNumber = accountNumber;
    }

    public string Execute(ConnectionContext context)
    {
        var dbContext = new BankDbContext();

        var account = dbContext.Accounts
            .Where(x => x.Number == accountNumber)
            .FirstOrDefault() ?? throw new InvalidOperationException("Account with the given number does not exist!");

        dbContext.Remove(account);

        dbContext.SaveChanges();
        
        return "";
    }
}