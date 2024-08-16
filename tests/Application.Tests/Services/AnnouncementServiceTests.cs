using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Moq;

namespace Application.Tests.Services;

public class AnnouncementServiceTests
{
    private readonly Mock<IAnnouncementRepository> _repositoryMock;
    private readonly AnnouncementService _service;

    public AnnouncementServiceTests()
    {
        _repositoryMock = new Mock<IAnnouncementRepository>();
        _service = new AnnouncementService(_repositoryMock.Object);
    }

    [Fact]
    public async Task AddAsync_ShouldSetDateAddedAndCallRepositoryAddAsync()
    {
        // Arrange
        var announcement = new Announcement
        {
            Id = Guid.NewGuid(),
            Title = "New Announcement",
            Description = "This is a new announcement."
        };

        // Act
        await _service.AddAsync(announcement);

        // Assert
        _repositoryMock.Verify(r => r.AddAsync(It.Is<Announcement>(a =>
            a.DateAdded <= DateTime.UtcNow &&
            a.Title == announcement.Title &&
            a.Description == announcement.Description)), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldCallRepositoryUpdateAsync()
    {
        // Arrange
        var announcement = new Announcement
        {
            Id = Guid.NewGuid(),
            Title = "Update Title",
            Description = "Update Description"
        };

        // Act
        await _service.UpdateAsync(announcement);

        // Assert
        _repositoryMock.Verify(r => r.UpdateAsync(announcement), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldCallRepositoryDeleteAsync()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        await _service.DeleteAsync(id);

        // Assert
        _repositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnListOfAnnouncements()
    {
        // Arrange
        var announcements = new List<Announcement>
            {
                new() { Id = Guid.NewGuid(), Title = "Title1", Description = "Desc1" },
                new() { Id = Guid.NewGuid(), Title = "Title2", Description = "Desc2" }
            };

        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(announcements);

        // Act
        var result = await _service.GetAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(result, a => a.Title == "Title1");
        Assert.Contains(result, a => a.Title == "Title2");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnAnnouncement_WhenExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var announcement = new Announcement { Id = id, Title = "Title", Description = "Description" };
        _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(announcement);

        // Act
        var result = await _service.GetByIdAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Title", result.Title);
        Assert.Equal("Description", result.Description);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenDoesNotExist()
    {
        // Arrange
        var id = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Announcement?)null);

        // Act
        var result = await _service.GetByIdAsync(id);

        // Assert
        Assert.Null(result);
    }
}