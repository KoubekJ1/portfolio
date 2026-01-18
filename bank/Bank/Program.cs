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

        var connectionContext = new ConnectionContext()
        {
            ServerIP = new System.Net.IPEndPoint(0, 0)
        };

        /*CreateAccountCommand command = new CreateAccountCommand();
        command.Execute(new ConnectionContext()
        {
            ServerIP = new System.Net.IPEndPoint(0, 0)
        });*/

        //var command = new AccountDepositCommand(10003, long.MaxValue);
        var command = new BankClientCountCommand();
        Console.WriteLine(command.Execute(connectionContext));
        
        /*for (int i = 11539; i < 100000; i++)
        {
            var account = new Account();
            account.Number = i;
            context.Accounts.Add(account);
        }
        context.SaveChanges();*/
    }
}
