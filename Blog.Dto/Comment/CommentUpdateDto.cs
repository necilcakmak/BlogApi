using System;
using System.ComponentModel.DataAnnotations;

public class CommentUpdateDto
{
    [Required(ErrorMessage = "Comment Id alanı boş olamaz.")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Yorum metni boş olamaz.")]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "Yorum metni uzunluğu 5 ile 500 karakter arasında olmalıdır.")]
    public string Text { get; set; }
}
