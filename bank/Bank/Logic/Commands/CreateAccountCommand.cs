using Bank.Domain;
using Bank.Persistence;
using Bank.Server;

namespace Bank.Logic.Commands;

public class CreateAccountCommand : ICommand
{
    public string Execute(ConnectionContext context)
    {
        using var dbContext = new BankDbContext();

        var newAccountNumber = Enumerable
            .Except(Enumerable.Range(10000, 89999), dbContext.Accounts.Select(x => x.Number))
            .FirstOrDefault();

        if (newAccountNumber == 0) throw new InvalidOperationException("Cannot create any more accounts!");

        var account = new Account();
        account.Number = newAccountNumber;

        dbContext.Accounts.Add(account);

        dbContext.SaveChanges();

        return $"{account.Number}/{context.ServerIP.Address}";
    }
}