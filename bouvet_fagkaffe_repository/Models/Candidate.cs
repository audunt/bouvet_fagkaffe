using System.ComponentModel.DataAnnotations;

namespace bouvet_fagkaffe_repository.Models;

public class Candidate
{
    public Guid Id { get; set; }
    [Required]
    public required string Topic { get; set; }
    [Required]
    public required string Summary { get; set; }
    public Guid ProposedBy { get; set; }
    public CandidateType Type { get; set; }
    public CandidateStatus Status { get; set; }
    public int Votes { get; set; }
    public List<Guid>? RegisteredPresenters { get; set; }
    public Format Format { get; set; }
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
    Physical
}
