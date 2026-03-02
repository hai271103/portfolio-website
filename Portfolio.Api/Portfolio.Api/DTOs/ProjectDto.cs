namespace Portfolio.Api.DTOs;

public class ProjectDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public string? DemoUrl { get; set; }
    public string? GitHubUrl { get; set; }
    public string Technologies { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsActive { get; set; }
}

public class ProjectCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public string? DemoUrl { get; set; }
    public string? GitHubUrl { get; set; }
    public string Technologies { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsFeatured { get; set; }
}

public class ProjectUpdateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public string? DemoUrl { get; set; }
    public string? GitHubUrl { get; set; }
    public string Technologies { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsActive { get; set; }
}
