using System.ComponentModel.DataAnnotations;

namespace bouvet_fagkaffe_repository.Models;

public class Candidate
{
    [Key]
    public Guid Id { get; set; } = new();
    [Required]
    public required string Topic { get; set; }
    [Required]
    public required string Summary { get; set; }
    public User? ProposedBy { get; set; }
    public CandidateType Type { get; set; }
    public CandidateStatus Status { get; set; } = CandidateStatus.Submitted;
    public int Votes { get; set; } = 0;
    public List<User> RegisteredPresenters { get; } = [];
    public Format Format { get; set; }
    public string? Department { get; set; }
}
public enum CandidateType
{
    Proposal,
    Request
}

public enum CandidateStatus
{
    Submitted,
    Accepted,
    Rejected,
    Retracted
}

public enum Format
{
    Online,
    Physical,
    Hybrid
}
