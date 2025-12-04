using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class UserUpdateDto : IValidatableObject
{
    [Required(ErrorMessage = "Ad alanı boş olamaz.")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Ad uzunluğu 5 ile 50 karakter arasında olmalıdır.")]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "Soyad alanı boş olamaz.")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Soyad uzunluğu 5 ile 50 karakter arasında olmalıdır.")]
    public required string LastName { get; set; }

    public bool Gender { get; set; }
    public bool NewBlog { get; set; }
    public bool PasswordIsChange { get; set; }

    public string? OldPassword { get; set; }

    public string? Password { get; set; }
    public string? PasswordRepeat { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (PasswordIsChange)
        {
            if (string.IsNullOrWhiteSpace(Password))
                yield return new ValidationResult("Şifre boş olamaz.", [nameof(Password)]);

            if (string.IsNullOrWhiteSpace(PasswordRepeat))
                yield return new ValidationResult("Şifre tekrar boş olamaz.", [nameof(PasswordRepeat)]);

            if (!string.IsNullOrWhiteSpace(Password) && Password.Length < 5 || Password.Length > 50)
                yield return new ValidationResult("Şifre uzunluğu 5 ile 50 karakter arasında olmalıdır.", [nameof(Password)]);

            if (Password != PasswordRepeat)
                yield return new ValidationResult("Şifre ve Şifre tekrarı eşleşmelidir.", [nameof(Password), nameof(PasswordRepeat)]);
        }
    }
}
