
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.Dto.Auth
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
