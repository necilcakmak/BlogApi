using System;
using System.ComponentModel.DataAnnotations;

public class ArticleAddDto
{
    [Required(ErrorMessage = "Başlık alanı boş olamaz.")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Başlık uzunluğu 5 ile 50 karakter arasında olmalıdır.")]
    public required string Title { get; set; }

    [Required(ErrorMessage = "İçerik alanı boş olamaz.")]
    [StringLength(5000, MinimumLength = 5, ErrorMessage = "İçerik uzunluğu 5 ile 5000 karakter arasında olmalıdır.")]
    public required string Content { get; set; }

    public required string Thumbnail { get; set; }

    public DateTime PublishedDate { get; set; }

    [Required(ErrorMessage = "Kategori seçimi zorunludur.")]
    public Guid CategoryId { get; set; }

    [Required(ErrorMessage = "Slug alanı boş olamaz.")]
    [StringLength(150, MinimumLength = 5, ErrorMessage = "Slug uzunluğu 5 ile 150 karakter arasında olmalıdır.")]
    public required string Slug { get; set; }

    [Required(ErrorMessage = "Keywords alanı boş olamaz.")]
    [StringLength(250, MinimumLength = 5, ErrorMessage = "Keywords uzunluğu 5 ile 250 karakter arasında olmalıdır.")]
    public required string Keywords { get; set; }
}
