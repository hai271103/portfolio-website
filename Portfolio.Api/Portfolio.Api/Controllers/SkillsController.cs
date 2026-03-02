using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Data;
using Portfolio.Api.DTOs;
using Portfolio.Api.Models;

namespace Portfolio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkillsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public SkillsController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all skills
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SkillDto>>> GetSkills()
    {
        var skills = await _context.Skills
            .Where(s => s.IsActive)
            .OrderBy(s => s.Category)
            .ThenBy(s => s.DisplayOrder)
            .ToListAsync();

        var skillDtos = _mapper.Map<List<SkillDto>>(skills);
        return Ok(skillDtos);
    }

    /// <summary>
    /// Get skill by id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<SkillDto>> GetSkill(int id)
    {
        var skill = await _context.Skills.FindAsync(id);

        if (skill == null)
        {
            return NotFound(new { message = $"Skill with id {id} not found" });
        }

        var skillDto = _mapper.Map<SkillDto>(skill);
        return Ok(skillDto);
    }

    /// <summary>
    /// Get skills by category
    /// </summary>
    [HttpGet("category/{category}")]
    public async Task<ActionResult<IEnumerable<SkillDto>>> GetSkillsByCategory(string category)
    {
        var skills = await _context.Skills
            .Where(s => s.Category == category && s.IsActive)
            .OrderBy(s => s.DisplayOrder)
            .ToListAsync();

        var skillDtos = _mapper.Map<List<SkillDto>>(skills);
        return Ok(skillDtos);
    }

    /// <summary>
    /// Create new skill
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<SkillDto>> CreateSkill(SkillCreateDto skillCreateDto)
    {
        var skill = _mapper.Map<Skill>(skillCreateDto);
        skill.CreatedAt = DateTime.UtcNow;
        skill.IsActive = true;

        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();

        var skillDto = _mapper.Map<SkillDto>(skill);
        return CreatedAtAction(nameof(GetSkill), new { id = skill.Id }, skillDto);
    }

    /// <summary>
    /// Update skill
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<SkillDto>> UpdateSkill(int id, SkillUpdateDto skillUpdateDto)
    {
        var skill = await _context.Skills.FindAsync(id);

        if (skill == null)
        {
            return NotFound(new { message = $"Skill with id {id} not found" });
        }

        _mapper.Map(skillUpdateDto, skill);
        await _context.SaveChangesAsync();

        var skillDto = _mapper.Map<SkillDto>(skill);
        return Ok(skillDto);
    }

    /// <summary>
    /// Delete skill
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSkill(int id)
    {
        var skill = await _context.Skills.FindAsync(id);

        if (skill == null)
        {
            return NotFound(new { message = $"Skill with id {id} not found" });
        }

        _context.Skills.Remove(skill);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
