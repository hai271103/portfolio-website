using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Data;
using Portfolio.Api.DTOs;

namespace Portfolio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProfileController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Get profile information
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ProfileDto>> GetProfile()
    {
        var profile = await _context.Profiles.FirstOrDefaultAsync();

        if (profile == null)
        {
            return NotFound(new { message = "Profile not found" });
        }

        var profileDto = _mapper.Map<ProfileDto>(profile);
        return Ok(profileDto);
    }

    /// <summary>
    /// Update profile information
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<ProfileDto>> UpdateProfile(ProfileUpdateDto profileUpdateDto)
    {
        var profile = await _context.Profiles.FirstOrDefaultAsync();

        if (profile == null)
        {
            return NotFound(new { message = "Profile not found" });
        }

        _mapper.Map(profileUpdateDto, profile);
        profile.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        var profileDto = _mapper.Map<ProfileDto>(profile);
        return Ok(profileDto);
    }
}
