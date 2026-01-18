using Bank.Persistence;
using Bank.Server;

namespace Bank.Logic.Commands;

public class AccountBalanceCommand : ICommand
{
    private int accountNumber;

    public AccountBalanceCommand(int accountNumber)
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

        var balance = account.Balance;

        return balance.ToString();
    }
}