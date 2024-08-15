using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AnnouncementsDbContext : DbContext
{
    public DbSet<Announcement> Announcements { get; set; }

    public AnnouncementsDbContext()
    {
    }

    public AnnouncementsDbContext(DbContextOptions<AnnouncementsDbContext> options) 
        : base(options)
    {
    }
}
