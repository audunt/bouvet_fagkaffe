using System.ComponentModel.DataAnnotations;

namespace bouvet_fagkaffe_repository.Models;

public class Tag
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public required string Value { get; set; }
    public List<Lecture> UsedOn { get; set; } = [];
}
