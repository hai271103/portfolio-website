namespace Portfolio.Api.DTOs;

public class SkillDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Hours { get; set; }
    public string? IconUrl { get; set; }
    public int Level { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}

public class SkillCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Hours { get; set; }
    public string? IconUrl { get; set; }
    public int Level { get; set; } = 1;
    public int DisplayOrder { get; set; }
}

public class SkillUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Hours { get; set; }
    public string? IconUrl { get; set; }
    public int Level { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}
