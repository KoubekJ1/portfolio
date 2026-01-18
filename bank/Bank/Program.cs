using Bank.Domain;
using Bank.Logic.Commands;
using Bank.Persistence;
using Bank.Server;

namespace Bank;

class Program
{
    static void Main(string[] args)
    {
        var context = new BankDbContext();
        context.Database.EnsureCreated();

        CreateAccountCommand command = new CreateAccountCommand();
        command.Execute(new ConnectionContext()
        {
            ServerIP = new System.Net.IPEndPoint(0, 0)
        });

        
        /*for (int i = 11539; i < 100000; i++)
        {
            var account = new Account();
            account.Number = i;
            context.Accounts.Add(account);
        }
        context.SaveChanges();*/
    }
}
