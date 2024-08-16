using Domain.Entities;

namespace Application.Interfaces;

public interface IAnnouncementService
{
    Task<List<Announcement>> GetAnnouncementsAsync();
    Task UpdateAnnouncementAsync(Announcement announcement);
    Task<List<Announcement>> GetSimilarAnnouncementsAsync(Guid id);
    Task DeleteAnnouncementAsync(Guid id);
}
