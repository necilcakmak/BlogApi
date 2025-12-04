using System;
using System.ComponentModel.DataAnnotations;

public class ParentCategoryAddDto
{
    [Required(ErrorMessage = "Kategori adı boş olamaz.")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Kategori adı uzunluğu 5 ile 50 karakter arasında olmalıdır.")]
    public string Name { get; set; }
}
