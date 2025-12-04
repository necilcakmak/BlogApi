
namespace Blog.Core.Results
{
    public class Result
    {
        public bool Success { get; }
        public string? Message { get; }
        public IDictionary<string, string[]>? ValidationErrors { get; set; }
        public Result(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        public Result(bool Success, string Message, IDictionary<string, string[]> validationErros)
        {
            this.Success = Success;
            this.Message = Message;
            this.ValidationErrors = validationErros;
        }
    }
}
