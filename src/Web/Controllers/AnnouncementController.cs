using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Web.Dtos;
using Web.Extensions;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnnouncementController : ControllerBase
{
    private readonly IAnnouncementService _announcementService;

    public AnnouncementController(IAnnouncementService announcementService)
    {
        _announcementService = announcementService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddAnnouncementAsync(AnnouncementDto announcement)
    {
        var result = await _announcementService.AddAsync(announcement.ToEntitity());
        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllAnnouncemetsAsync()
    {
        return Ok(await _announcementService.GetAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAnnouncementByIdAsync(Guid id)
    {
        return Ok(await _announcementService.GetByIdAsync(id));
    }

    [HttpGet("similar/{id}")]
    public async Task<IActionResult> GetSimilarAnnouncementsAsync(Guid id)
    {
        var announcement = await _announcementService.GetByIdAsync(id);

        if (announcement == null)
        {
            return NotFound();
        }

        var similarAnnouncements = await _announcementService.GetSimilarAnnouncementsAsync(id);

        return Ok(similarAnnouncements);

    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAnnouncementAsync(Guid id, [FromBody] Announcement announcement)
    {
        if (id != announcement.Id)
        {
            return BadRequest();
        }

        await _announcementService.UpdateAsync(announcement);
        return NoContent();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAnnouncementAsync(Guid id)
    {
        await _announcementService.DeleteAsync(id);
        return NoContent();
    }
}
