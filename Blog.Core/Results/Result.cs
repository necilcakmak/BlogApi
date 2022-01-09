
namespace Blog.Core.Results
{
    public class Result
    {
        public bool Success { get; }
        public string? Message { get; }
        public Dictionary<string, string>? ValidationErrors { get; set; }
        public Result(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }

    }
}
