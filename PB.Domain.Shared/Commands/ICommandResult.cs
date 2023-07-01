namespace PB.Domain.Shared.Commands
{
    public interface ICommandResult
    {
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}