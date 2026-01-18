using Bank.Server;

namespace Bank.Logic;

public interface ICommand
{
    string Execute(ConnectionContext context);
}