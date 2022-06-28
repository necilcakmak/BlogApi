using Blog.Core.Results;
using Blog.Dto.Auth;
using Blog.Dto.Token;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Blog.Ui.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient client;

        public HttpClientService(HttpClient client)
        {
            this.client = client;
        }
        public async Task<T> Post<T, R>(string url, R postData)
        {
            var httpResponse = await client.PostAsJsonAsync(url, postData);
            var res = await httpResponse.Content.ReadFromJsonAsync<T>();
            return res;
        }
       
    }
}
