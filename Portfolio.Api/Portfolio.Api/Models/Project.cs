using System.ComponentModel.DataAnnotations;

namespace Portfolio.Api.Models;

public class Project
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Role { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? ThumbnailUrl { get; set; }

    [MaxLength(500)]
    public string? DemoUrl { get; set; }

    [MaxLength(500)]
    public string? GitHubUrl { get; set; }

    [MaxLength(500)]
    public string Technologies { get; set; } = string.Empty; // JSON array

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public int DisplayOrder { get; set; } = 0;

    public bool IsFeatured { get; set; } = false;
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public ICollection<ProjectImage> ProjectImages { get; set; } = new List<ProjectImage>();
}
