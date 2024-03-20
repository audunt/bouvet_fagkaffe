using bouvet_fagkaffe_repository.Context;
using bouvet_fagkaffe_repository.Models;
using Microsoft.EntityFrameworkCore;

namespace bouvet_fagkaffe_repository;

public class Operations(FagkaffeContext context)
{
    private readonly FagkaffeContext _context = context;

    #region Candidate
    public ValueTask<Candidate?> GetCandidate(Guid id)
    {
        return _context.Candidates.FindAsync(id);
    }

    public Task<List<Candidate>> GetAllCandidates()
    {
        return _context.Candidates.ToListAsync();
    }

    public Task<List<Candidate>> GetAllCandidatesByDepartments(List<string?> department)
    {
        return _context.Candidates.Where(c => 
            c.Department != null &&
            department.Contains(c.Department) &&
            c.Status == CandidateStatus.Submitted)
            .ToListAsync();
    }

    public Task<List<Candidate>> GetAllCandidatesNoDepartment()
    {
        return _context.Candidates.Where(c => 
            c.Department == null &&
            c.Status == CandidateStatus.Submitted)
            .ToListAsync();
    }

    public Task<List<Candidate>> GetAllCandidatesForUser(User user)
    {
        return _context.Candidates.Where(c => 
            user.Groups.Contains(c.Department) && c.Status == CandidateStatus.Submitted)
            .Include(u => u.RegisteredPresenters)
            .Include(u => u.VotedOnBy)
            .ToListAsync();
    }

    public async Task<Candidate> CreateCandidate(Candidate candidate)
    {
        await _context.Candidates.AddAsync(candidate);
        await _context.SaveChangesAsync();
        return candidate;
    }

    public async Task<Candidate> UpdateCandidate(Candidate candidate)
    {
        var storedCandidate = await GetCandidate(candidate.Id);
        if (storedCandidate != null)
        {
            storedCandidate = candidate;
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("failed updating the candidate");
        }
        return storedCandidate;
    }

    #endregion

    #region Lecture
    public ValueTask<Lecture?> GetLecture(Guid id)
    {
        return _context.Lectures.FindAsync(id);
    }

    public Task<List<Lecture>> GetAllLecturesForUser(User user)
    {
        return _context.Lectures.Where(l =>
                user.Groups.Contains(l.Department))
                .Include(u => u.HeldBy)
                .Include(m => m.MeetingLinks)
                .Include(t => t.Tags)
                .ToListAsync();
    }

    public Task<List<Lecture>> GetAllLecturesByDepartments(List<string?> department)
    {
        return _context.Lectures.Where(l =>
            l.Department != null && department.Contains(l.Department))
            .Include(u => u.HeldBy)
            .Include(m => m.MeetingLinks)
            .Include(t => t.Tags)
            .ToListAsync();
    }

    public Task<List<Lecture>> GetAllLecturesNoDepartment()
    {
        return _context.Lectures.Where(l =>
            l.Department == null)
            .Include(u => u.HeldBy)
            .Include(m => m.MeetingLinks)
            .Include(t => t.Tags)
            .ToListAsync();
    }

    public Task<List<Lecture>> GetAllDisplayLecturesForUser(User user)
    {
        return _context.Lectures.Where(l =>
                user.Groups.Contains(l.Department) && l.Status == LectureStatus.Planned)
                .Include(u => u.HeldBy)
                .Include(m => m.MeetingLinks)
                .Include(t => t.Tags)
                .OrderBy(l => l.HeldAt)
                .ToListAsync();
    }

    public Task<List<Lecture>> GetAllDisplayLecturesByDepartments(List<string?> department)
    {
        return _context.Lectures.Where(l =>
            l.Department != null && department.Contains(l.Department) && l.Status == LectureStatus.Planned)
            .Include(u => u.HeldBy)
            .Include(m => m.MeetingLinks)
            .Include(t => t.Tags)
            .OrderBy(l => l.HeldAt)
            .ToListAsync();
    }

    public Task<List<Lecture>> GetAllDisplayLecturesNoDepartment()
    {
        return _context.Lectures.Where(l =>
            l.Department == null && l.Status == LectureStatus.Planned)
            .Include(u => u.HeldBy)
            .Include(m => m.MeetingLinks)
            .Include(t => t.Tags)
            .ToListAsync();
    }

    public async Task<Lecture> CreateLecture(Lecture lecture)
    {
        await _context.Lectures.AddAsync(lecture);
        await _context.SaveChangesAsync();
        return lecture;
    }

    public async Task<Lecture> CreateLectureFromCandidate(Lecture lecture, Guid candidateId)
    {
        var candidate = await GetCandidate(candidateId);
        if (candidate == null)
            throw new Exception("Unable to find existing candidate");

        candidate.Status = CandidateStatus.Accepted;
        await UpdateCandidate(candidate);
        await _context.Lectures.AddAsync(lecture);
        await _context.SaveChangesAsync();

        return lecture;
    }

    public async Task<Lecture> UpdateLecture(Lecture lecture)
    {
        var storedLecture = await GetLecture(lecture.Id);
        if (storedLecture != null)
        {
            storedLecture = lecture;
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Failed to update lecture");
        }
        return storedLecture;

    }
    #endregion

    #region User
    public ValueTask<User?> GetUser(Guid id)
    {
        return  _context.Users.FindAsync(id);
    }

    public Task<User?> GetUserByForeignId(string foreignId)
    {
        return _context.Users.FirstOrDefaultAsync(u  => u.ForeignId == foreignId);
    }


    public async Task<User> CreateUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUser(User user)
    {
        var storedUser = await GetUser(user.Id);
        if (storedUser != null)
        {
            storedUser = user;
            await _context.SaveChangesAsync();
        }
        else
            throw new Exception("failed updating the user");
        return storedUser;
    }
    #endregion

    #region Tags
    public async Task<Tag> GetTag(string value)
    {
        value = value.Trim().ToLower();
        var tag =  await _context.Tags.FirstOrDefaultAsync(t => t.Value == value);
        if (tag != null)
            return tag;
        else
        {
            tag = new Tag() { Value = value };
            await _context.AddAsync(tag);
            await _context.SaveChangesAsync();
            return tag;
        }
    }

    public async Task<Lecture> AddTagToLecture(string value, Guid lectureId)
    {
        var tag = await GetTag(value);
        var lecture = await GetLecture(lectureId);
        if (lecture != null)
        {
            lecture.Tags.Add(tag);
            lecture = await UpdateLecture(lecture);
            return lecture;
        }
        else
            throw new Exception("Couldnt add the tag to lecture");
    }


    #endregion
}
