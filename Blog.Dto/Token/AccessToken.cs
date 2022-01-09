

using Blog.Dto.User;

namespace Blog.Dto.Token
{
    public class AccessToken
    {
        public string? Token { get; set; }
        public DateTime? Expiration { get; set; }
        public UserDto? User { get; set; }
    }
}
