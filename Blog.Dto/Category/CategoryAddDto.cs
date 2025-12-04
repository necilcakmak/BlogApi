using System;
using System.ComponentModel.DataAnnotations;

public class CategoryAddDto
{
    [Required(ErrorMessage = "Kategori adı boş olamaz.")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Kategori adı uzunluğu 5 ile 50 karakter arasında olmalıdır.")]
    public string? Name { get; set; }

    public string TagName { get; set; }

    public Guid ParentCategoryId { get; set; }
}
