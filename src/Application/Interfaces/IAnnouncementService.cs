using Domain.Entities;

namespace Application.Interfaces;

public interface IAnnouncementService
{
    Task<Announcement> AddAsync(Announcement announcement);
    Task<List<Announcement>> GetAsync();
    Task<Announcement?> GetByIdAsync(Guid id);
    Task UpdateAsync(Announcement announcement);
    Task DeleteAsync(Guid id);
    Task<List<Announcement>> GetSimilarAnnouncementsAsync(Guid id);
}
