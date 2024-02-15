using System.ComponentModel.DataAnnotations;

namespace bouvet_fagkaffe_repository.Models;

public class User
{
    [Key]
    public Guid Id { get; set; } = new();
    [Required]
    public required string ForeignId { get; set; }
    [Required]
    public required string FirstName { get; set; }
    [Required]
    public required string LastName { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    public List<string?> Groups { get; set; } = [];
    [Required]
    public bool IsAdmin { get; set; } = false;
    public List<Candidate> RegisteredOnCandidates { get; } = [];
    public List<Lecture> PresentsLectures { get; } = [];
}
