using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Data;
using Portfolio.Api.DTOs;

namespace Portfolio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SocialLinksController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public SocialLinksController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all social links
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SocialLinkDto>>> GetSocialLinks()
    {
        var socialLinks = await _context.SocialLinks
            .OrderBy(s => s.DisplayOrder)
            .ToListAsync();

        var socialLinkDtos = _mapper.Map<List<SocialLinkDto>>(socialLinks);
        return Ok(socialLinkDtos);
    }

    /// <summary>
    /// Get active social links only
    /// </summary>
    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<SocialLinkDto>>> GetActiveSocialLinks()
    {
        var socialLinks = await _context.SocialLinks
            .Where(s => s.IsActive)
            .OrderBy(s => s.DisplayOrder)
            .ToListAsync();

        var socialLinkDtos = _mapper.Map<List<SocialLinkDto>>(socialLinks);
        return Ok(socialLinkDtos);
    }
}
