using Domain.Entities;
using Web.Dtos;

namespace Web.Extensions;

public static class AnnouncementDtoExtensions
{
    public static Announcement ToEntitity(this AnnouncementDto dto)
    {
        return new()
        {
            Title = dto.Title,
            Description = dto.Description,
            DateAdded = DateTime.UtcNow,
        };
    }
}
