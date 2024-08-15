namespace Domain.Entities;

public class Announcement
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime DateAdded { get; set; }
}
