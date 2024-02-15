using System.ComponentModel.DataAnnotations;

namespace bouvet_fagkaffe_repository.Models;

public class MeetingLink
{
    [Key]
    public Guid Id { get; set; } = new();
    [Required]
    public required string Description { get; set; }
    [Required]
    public required string Link { get; set; }
}
