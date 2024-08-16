using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Tests.DataTests;

public class AnnouncementRepositoryTests
{
    private readonly AnnouncementsDbContext _context;
    private readonly AnnouncementRepository _repository;

    public AnnouncementRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AnnouncementsDbContext>()
            .UseInMemoryDatabase(databaseName: "AnnouncementsTestDb")
            .Options;

        _context = new AnnouncementsDbContext(options);
        _repository = new AnnouncementRepository(_context);
    }

    [Fact]
    public async Task AddAsync_ShouldAddAnnouncementToDatabase()
    {
        // Arrange
        var announcement = new Announcement
        {
            Id = Guid.NewGuid(),
            Title = "New Announcement",
            Description = "This is a new announcement."
        };

        // Act
        await _repository.AddAsync(announcement);

        // Assert
        var addedAnnouncement = await _context.Announcements.FindAsync(announcement.Id);
        Assert.NotNull(addedAnnouncement);
        Assert.Equal(announcement.Title, addedAnnouncement.Title);
        Assert.Equal(announcement.Description, addedAnnouncement.Description);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllAnnouncements()
    {
        // Arrange
        var announcement1 = new Announcement { Id = Guid.NewGuid(), Title = "Title1", Description = "Desc1" };
        var announcement2 = new Announcement { Id = Guid.NewGuid(), Title = "Title2", Description = "Desc2" };

        _context.Announcements.RemoveRange(_context.Announcements);
        await _context.SaveChangesAsync();

        _context.Announcements.AddRange(announcement1, announcement2);
        await _context.SaveChangesAsync();

        // Act
        var announcements = await _repository.GetAllAsync();

        // Assert
        Assert.Equal(2, announcements.Count);
        Assert.Contains(announcements, a => a.Id == announcement1.Id);
        Assert.Contains(announcements, a => a.Id == announcement2.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnAnnouncement_WhenAnnouncementExists()
    {
        // Arrange
        var announcement = new Announcement { Id = Guid.NewGuid(), Title = "Title", Description = "Description" };
        _context.Announcements.Add(announcement);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(announcement.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(announcement.Title, result.Title);
        Assert.Equal(announcement.Description, result.Description);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenAnnouncementDoesNotExist()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var result = await _repository.GetByIdAsync(nonExistentId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateAnnouncement()
    {
        // Arrange
        var announcement = new Announcement { Id = Guid.NewGuid(), Title = "Original Title", Description = "Original Description" };
        _context.Announcements.Add(announcement);
        await _context.SaveChangesAsync();

        announcement.Title = "Updated Title";
        announcement.Description = "Updated Description";

        // Act
        await _repository.UpdateAsync(announcement);

        // Assert
        var updatedAnnouncement = await _context.Announcements.FindAsync(announcement.Id);
        Assert.NotNull(updatedAnnouncement);
        Assert.Equal("Updated Title", updatedAnnouncement.Title);
        Assert.Equal("Updated Description", updatedAnnouncement.Description);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveAnnouncement()
    {
        // Arrange
        var announcement = new Announcement { Id = Guid.NewGuid(), Title = "Title", Description = "Description" };
        _context.Announcements.Add(announcement);
        await _context.SaveChangesAsync();

        // Act
        await _repository.DeleteAsync(announcement.Id);

        // Assert
        var deletedAnnouncement = await _context.Announcements.FindAsync(announcement.Id);
        Assert.Null(deletedAnnouncement);
    }
}
