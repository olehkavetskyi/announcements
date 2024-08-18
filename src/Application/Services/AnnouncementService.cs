using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class AnnouncementService : IAnnouncementService
{
    private readonly IAnnouncementRepository _announcementRepository;

    public AnnouncementService(IAnnouncementRepository announcementRepository)
    {
        _announcementRepository = announcementRepository;
    }

    public async Task<Announcement> AddAsync(Announcement announcement)
    {
        await _announcementRepository.AddAsync(announcement);
        return await _announcementRepository.GetByIdAsync(announcement.Id);
    }

    public async Task UpdateAsync(Announcement announcement)
    {
        await _announcementRepository.UpdateAsync(announcement);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _announcementRepository.DeleteAsync(id);
    }

    public async Task<List<Announcement>> GetAsync()
    {
        return await _announcementRepository.GetAllAsync();
    }

    public async Task<Announcement?> GetByIdAsync(Guid id)
    {
        return await _announcementRepository.GetByIdAsync(id);
    }

    public async Task<List<Announcement>> GetSimilarAnnouncementsAsync(Guid id)
    {
        var targetAnnouncement = await _announcementRepository.GetByIdAsync(id);

        if (targetAnnouncement == null)
        {
            return [];
        }

        var potentialMatches = await _announcementRepository.GetAllAsync();

        var similarAnnouncements = potentialMatches
            .Where(a => a.Id != id && HasSimilarWords(targetAnnouncement.Title, a.Title) &&
                        HasSimilarWords(targetAnnouncement.Description, a.Description))
            .Take(3)
            .ToList();

        if (similarAnnouncements == null)
        {
            return [];
        }

        return similarAnnouncements;
    }

    private bool HasSimilarWords(string text1, string text2)
    {
        var words1 = text1?.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? [];
        var words2 = text2?.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? [];
        return words1.Intersect(words2).Any();
    }
}
