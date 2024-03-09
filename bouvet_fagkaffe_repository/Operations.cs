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
            user.Groups.Contains(c.Department))
            .Include(u => u.RegisteredPresenters)
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
                .ToListAsync();
    }

    public Task<List<Lecture>> GetAllLecturesByDepartments(List<string?> department)
    {
        return _context.Lectures.Where(l =>
            l.Department != null && department.Contains(l.Department))
            .ToListAsync();
    }

    public Task<List<Lecture>> GetAllLecturesNoDepartment()
    {
        return _context.Lectures.Where(l =>
            l.Department == null)
            .ToListAsync();
    }

    public async Task<Lecture> CreateLecture(Lecture lecture)
    {
        await _context.Lectures.AddAsync(lecture);
        await _context.SaveChangesAsync();
        return lecture;
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
}
