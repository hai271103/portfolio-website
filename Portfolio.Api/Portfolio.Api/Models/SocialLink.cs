using System.ComponentModel.DataAnnotations;

namespace Portfolio.Api.Models;

public class SocialLink
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Platform { get; set; } = string.Empty; // GitHub, LinkedIn, Facebook, etc.

    [Required]
    [MaxLength(500)]
    public string Url { get; set; } = string.Empty;

    [MaxLength(100)]
    public string IconClass { get; set; } = string.Empty; // Font Awesome class

    public int DisplayOrder { get; set; } = 0;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
