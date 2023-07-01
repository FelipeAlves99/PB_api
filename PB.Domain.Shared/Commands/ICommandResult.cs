namespace PB.Domain.Shared.Commands
{
    public interface ICommandResult
    {
        public string Message { get; set; }
        public string Success { get; set; }
    }
}