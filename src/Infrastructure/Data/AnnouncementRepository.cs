using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AnnouncementRepository : IAnnouncementRepository
{
    private readonly AnnouncementsDbContext _context;

    public AnnouncementRepository(AnnouncementsDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Announcement announcement)
    {
        _context.Announcements.Add(announcement);
        await _context.SaveChangesAsync();
    }

    public IQueryable<Announcement> GetAll()
    {
        return _context.Announcements.AsQueryable();
    }

    public async Task<Announcement?> GetByIdAsync(Guid id)
    {
        return await _context.Announcements.FindAsync(id);
    }

    public async Task UpdateAsync(Announcement announcement)
    {
        _context.Announcements.Update(announcement);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var announcement = await GetByIdAsync(id);

        if (announcement != null)
        {
            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync();
        }
    }
}
