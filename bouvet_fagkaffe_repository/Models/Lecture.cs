using System.ComponentModel.DataAnnotations;

namespace bouvet_fagkaffe_repository.Models;

public class Lecture
{
    public Guid Id { get; set; }
    [Required]
    public required string Title { get; set; }
    [Required]
    public required string Topic { get; set; }
    [Required]
    public required string Summary { get; set; }
    [Required]
    public required List<Guid> HeldBy { get; set; }
    [DataType(DataType.Date)]
    public DateTimeOffset HeldAt { get; set; }
    public LectureStatus Status { get; set; }
    public List<MeetingLink>? MeetingLinks { get; set; }
    public List<string>? Tags { get; set; }
}

public enum LectureStatus
{
    Accepted,
    Planned,
    Completed,
    Cancelled
}
