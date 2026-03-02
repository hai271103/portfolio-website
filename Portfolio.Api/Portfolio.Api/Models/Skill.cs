using System.ComponentModel.DataAnnotations;

namespace Portfolio.Api.Models;

public class Skill
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Category { get; set; } = string.Empty;

    public int Hours { get; set; } = 0;

    [MaxLength(500)]
    public string? IconUrl { get; set; }

    [Range(1, 5)]
    public int Level { get; set; } = 1;

    public int DisplayOrder { get; set; } = 0;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
