using Domain.Entities;

namespace Application.Interfaces;

public interface IAnnouncementRepository
{
    Task<List<Announcement>> GetAllAsync();
    Task AddAsync(Announcement announcement);
    Task<Announcement?> GetByIdAsync(Guid id);
    Task UpdateAsync(Announcement announcement);
    Task DeleteAsync(Guid id);
}
