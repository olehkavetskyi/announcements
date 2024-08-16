using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Application.Services;

public class AnnouncementService : IAnnouncementService
{
    private readonly IAnnouncementRepository _announcementRepository;

    public AnnouncementService(IAnnouncementRepository announcementRepository)
    {
        _announcementRepository = announcementRepository;
    }

    public async Task DeleteAnnouncementAsync(Guid id)
    {
        await _announcementRepository.DeleteAsync(id);
    }

    public async Task<List<Announcement>> GetAnnouncementsAsync()
    {
        return await _announcementRepository.GetAll().ToListAsync();
    }

    public async Task<List<Announcement>> GetSimilarAnnouncementsAsync(Guid id)
    {
        var targetAnnouncement = await _announcementRepository.GetByIdAsync(id);

        if (targetAnnouncement == null)
        {
            return [];
        }

        var similarAnnouncements = await _announcementRepository.GetAll()
            .Where(a => a.Id != id &&
                        (HasSimilarWords(targetAnnouncement.Title, a.Title) ||
                         HasSimilarWords(targetAnnouncement.Description, a.Description)))
            .Take(3)
            .ToListAsync();

        if (similarAnnouncements == null)
        {
            return [];
        }

        return similarAnnouncements;
    }

    public async Task UpdateAnnouncementAsync(Announcement announcement)
    {
        await _announcementRepository.UpdateAsync(announcement);
    }

    private bool HasSimilarWords(string text1, string text2)
    {
        var words1 = text1?.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? [];
        var words2 = text2?.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? [];
        return words1.Intersect(words2).Any();
    }
}
