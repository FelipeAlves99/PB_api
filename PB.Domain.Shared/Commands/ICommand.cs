using MediatR;

namespace PB.Domain.Shared.Commands
{
    public interface ICommand : IRequest<ICommandResult>
    { }
}