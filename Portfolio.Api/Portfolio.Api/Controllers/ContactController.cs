using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Data;
using Portfolio.Api.DTOs;
using Portfolio.Api.Models;

namespace Portfolio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<ContactController> _logger;

    public ContactController(ApplicationDbContext context, IMapper mapper, ILogger<ContactController> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Send contact message
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ContactMessageDto>> SendContactMessage(ContactMessageCreateDto contactMessageCreateDto)
    {
        var contactMessage = _mapper.Map<ContactMessage>(contactMessageCreateDto);
        contactMessage.CreatedAt = DateTime.UtcNow;
        contactMessage.IsRead = false;

        _context.ContactMessages.Add(contactMessage);
        await _context.SaveChangesAsync();

        _logger.LogInformation("New contact message received from {Email}", contactMessage.Email);

        var contactMessageDto = _mapper.Map<ContactMessageDto>(contactMessage);
        return Ok(new 
        { 
            message = "Your message has been sent successfully!",
            data = contactMessageDto
        });
    }

    /// <summary>
    /// Get all contact messages (Admin only)
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContactMessageDto>>> GetContactMessages()
    {
        var messages = await _context.ContactMessages
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();

        var messageDtos = _mapper.Map<List<ContactMessageDto>>(messages);
        return Ok(messageDtos);
    }

    /// <summary>
    /// Get unread messages count (Admin only)
    /// </summary>
    [HttpGet("unread/count")]
    public async Task<ActionResult<int>> GetUnreadCount()
    {
        var count = await _context.ContactMessages
            .Where(m => !m.IsRead)
            .CountAsync();

        return Ok(new { count });
    }

    /// <summary>
    /// Mark message as read (Admin only)
    /// </summary>
    [HttpPut("{id}/read")]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        var message = await _context.ContactMessages.FindAsync(id);

        if (message == null)
        {
            return NotFound(new { message = $"Message with id {id} not found" });
        }

        message.IsRead = true;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Message marked as read" });
    }
}
