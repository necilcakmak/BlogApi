using System.ComponentModel.DataAnnotations;

public class RegisterDto
{
    [Required(ErrorMessage = "Email alanı boş olamaz.")]
    [EmailAddress(ErrorMessage = "Lütfen geçerli bir email adresi girin.")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Email uzunluğu 5 ile 50 karakter arasında olmalıdır.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Kullanıcı adı alanı boş olamaz.")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Kullanıcı adı uzunluğu 5 ile 50 karakter arasında olmalıdır.")]
    public required string UserName { get; set; }

    [Required(ErrorMessage = "Ad alanı boş olamaz.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Ad uzunluğu 3 ile 50 karakter arasında olmalıdır.")]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "Soyad alanı boş olamaz.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Soyad uzunluğu 2 ile 50 karakter arasında olmalıdır.")]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "Şifre alanı boş olamaz.")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Şifre uzunluğu 5 ile 50 karakter arasında olmalıdır.")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Doğum tarihi alanı boş olamaz.")]
    [CustomValidation(typeof(RegisterDto), nameof(ValidateAge))]
    public DateTime BirthDate { get; set; }
    public bool Gender { get; set; }

    // Custom validation metodu
    public static ValidationResult? ValidateAge(DateTime? dateOfBirth, ValidationContext context)
    {
        if (!dateOfBirth.HasValue)
            return new ValidationResult("Doğum tarihi boş olamaz.");

        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Value.Year;
        if (dateOfBirth.Value.Date > today.AddYears(-age)) age--;

        if (age < 18 || age > 65)
            return new ValidationResult("Yaş 18 ile 65 arasında olmalıdır.");

        return ValidationResult.Success;
    }
}
