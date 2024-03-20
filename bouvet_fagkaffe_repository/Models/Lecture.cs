using System.ComponentModel.DataAnnotations;

namespace bouvet_fagkaffe_repository.Models;

public class Lecture
{
    [Key]
    public Guid Id { get; set; } = new();
    [Required]
    public required string Title { get; set; }
    [Required]
    public required string Topic { get; set; }
    [Required]
    public required string Summary { get; set; }
    [Required]
    public List<User> HeldBy { get; } = [];
    [DataType(DataType.Date)]
    public DateTimeOffset? HeldAt { get; set; }
    public string? Location { get; set; }
    public LectureStatus Status { get; set; } = LectureStatus.Accepted;
    public List<MeetingLink> MeetingLinks { get; } = [];
    public List<string> Tags { get; set; } = [];
    public string? Department { get; set; }
    public bool ApprovedByPresenter { get; set; } = false;
    public bool ApprovedByAdmin { get; set; } = false;
}

public enum LectureStatus
{
    Accepted,
    Planned,
    Completed,
    Cancelled
}
