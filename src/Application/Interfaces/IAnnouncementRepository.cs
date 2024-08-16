using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IAnnouncementRepository
{
    IQueryable<Announcement> GetAll();
    Task AddAsync(Announcement announcement);
    Task<Announcement?> GetByIdAsync(Guid id);
    Task UpdateAsync(Announcement announcement);
    Task DeleteAsync(Guid id);
}
