using Bank.Persistence;
using Bank.Server;

namespace Bank.Logic.Commands;

public class AccountDepositCommand : ICommand
{
    private int accountNumber;
    private long amount;

    public AccountDepositCommand(int accountNumber, long amount)
    {
        if (accountNumber < 10000 || accountNumber > 99999)
        {
            throw new ArgumentException("Invalid account number!");
        }
        this.accountNumber = accountNumber;

        if (amount < 1)
        {
            throw new ArgumentException("Amount must be larger than 0!");
        }
        this.amount = amount;
    }

    public string Execute(ConnectionContext context)
    {
        var dbContext = new BankDbContext();

        var account = dbContext.Accounts
            .Where(x => x.Number == accountNumber)
            .FirstOrDefault() ?? throw new InvalidOperationException("Account with the given number does not exist!");

        if (account.Balance + amount > long.MaxValue || account.Balance + amount < 0)
        {
            throw new InvalidOperationException("Not enough room in the account for the deposited amount!");
        }

        account.Balance += amount;

        dbContext.SaveChanges();

        return "";
    }
}