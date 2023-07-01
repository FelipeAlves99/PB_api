namespace PB.Domain.Shared.Commands
{
    public class Command<T> : ICommand
        where T : Command<T>
    { }
}
