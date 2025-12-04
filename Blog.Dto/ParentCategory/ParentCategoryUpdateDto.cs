using System;
using System.ComponentModel.DataAnnotations;

public class ParentCategoryUpdateDto
{
    [Required(ErrorMessage = "Kategori Id alanı boş olamaz.")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Kategori adı boş olamaz.")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Kategori adı uzunluğu 5 ile 50 karakter arasında olmalıdır.")]
    public string Name { get; set; }
}
