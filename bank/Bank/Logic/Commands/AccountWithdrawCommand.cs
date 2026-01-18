using Bank.Persistence;
using Bank.Server;

namespace Bank.Logic.Commands;

public class AccountWithdrawCommand : ICommand
{
    private int accountNumber;
    private long amount;

    public AccountWithdrawCommand(int accountNumber, long amount)
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

        if (amount > account.Balance)
        {
            throw new InvalidOperationException("Not enough funds on the account!");
        }

        account.Balance -= amount;

        dbContext.SaveChanges();

        return "";
    }
}