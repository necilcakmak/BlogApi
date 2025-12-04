
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.Dto.Auth
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email alanı boş olamaz.")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir email adresi girin.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Email uzunluğu 5 ile 50 karakter arasında olmalıdır.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunlu.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Şifre uzunluğu 5 ile 50 karakter arasında olmalıdır.")]
        public required string Password { get; set; }
    }
}
