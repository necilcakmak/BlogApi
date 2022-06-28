using Blog.Core.Results;
using Blog.Dto.Auth;
using Blog.Dto.Token;

namespace Blog.Ui.Services
{
    public interface IHttpClientService
    {
        Task<T> Post<T, R>(string url, R postData);
    }
}