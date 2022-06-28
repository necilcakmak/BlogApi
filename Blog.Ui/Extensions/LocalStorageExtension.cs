using Blazored.LocalStorage;
using Blog.Dto.User;
using System.Text.Json;

namespace Blog.Ui.Extensions
{
    public static class LocalStorageExtension
    {
        public const string TokenName = "token";
        public const string User = "user";

        public static bool IsUserLoggedIn(this ISyncLocalStorageService localStorageService)
        {
            return !string.IsNullOrEmpty(GetToken(localStorageService));
        }

        public static async Task<string> GetToken(this ILocalStorageService localStorageService)
        {
            var token = await localStorageService.GetItemAsync<string>(TokenName);
            return token;
        }
        public static string GetToken(this ISyncLocalStorageService localStorageService)
        {
            var token = localStorageService.GetItem<string>(TokenName);
            return token;
        }

        public static void SetToken(this ISyncLocalStorageService localStorageService, string value)
        {
            localStorageService.SetItem(TokenName, value);
        }

        public static async Task SetToken(this ILocalStorageService localStorageService, string value)
        {
            await localStorageService.SetItemAsync(TokenName, value);
        }
    }
}
