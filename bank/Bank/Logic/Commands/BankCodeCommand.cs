using Bank.Server;

namespace Bank.Logic.Commands;

public class BankCodeCommand : ICommand
{
    public string Execute(ConnectionContext context)
    {
        return context.ServerIP.Address.ToString();
    }
}