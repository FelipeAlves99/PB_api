namespace PB.Domain.Shared.Commands
{
    public class CommandResult<T> : ICommandResult
        where T : class
    {
        protected CommandResult()
        { }

        public CommandResult(string message, string success)
        {
            Message = message;
            Success = success;
        }

        public CommandResult(string message, string success, T data) : this(message, success)
        {
            Data = data;
        }

        public string? Message { get; set; }
        public string? Success { get; set; }
        public T? Data { get; set; }
    }
}