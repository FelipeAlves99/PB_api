namespace PB.Domain.Shared.Commands
{
    public class CommandResult<T> : ICommandResult
        where T : class
    {
        protected CommandResult()
        { }

        public CommandResult(string message, bool success, T data)
        {
            Message = message;
            Success = success;
            Data = data;
        }

        public string Message { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }
    }
}