using MediatR;

namespace PB.Domain.Shared.Commands
{
    public interface ICommandHandler<T> : IRequestHandler<T, ICommandResult>
        where T : Command<T>
    { }
}