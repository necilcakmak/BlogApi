
namespace Blog.Core.Results
{
    public class DataResult<T> : Result
    {
        public T Data { get; }
        public DataResult(T Data, bool Success, string Message) : base(Success, Message)
        {
            this.Data = Data;
        }
    }
}
