using System;
using System.ComponentModel.DataAnnotations;

public class CommentAddDto
{
    [Required(ErrorMessage = "ArticleId alanı boş olamaz.")]
    public Guid ArticleId { get; set; }

    [Required(ErrorMessage = "Yorum metni boş olamaz.")]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "Yorum metni uzunluğu 5 ile 500 karakter arasında olmalıdır.")]
    public string? Text { get; set; }
}
