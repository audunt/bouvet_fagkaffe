using System.ComponentModel.DataAnnotations;

namespace bouvet_fagkaffe_repository.Models;

public class MeetingLink
{
    public Guid Id { get; set; }
    [Required]
    public required string Description { get; set; }
    [Required]
    public required string Link { get; set; }
}
