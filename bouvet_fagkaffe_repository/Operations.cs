using bouvet_fagkaffe_repository.Context;
using bouvet_fagkaffe_repository.Models;
using Microsoft.EntityFrameworkCore;

namespace bouvet_fagkaffe_repository;

public class Operations(FagkaffeContext context)
{
    private readonly FagkaffeContext _context = context;

    #region Get
    public ValueTask<Candidate?> GetCandidates(Guid id)
    {
        return _context.Candidates.FindAsync(id);
    }

    public Task<List<Candidate>> GetAllCandidates()
    {
        return _context.Candidates.ToListAsync();
    }

    public ValueTask<Lecture?> GetLectures(Guid id)
    {
        return _context.Lectures.FindAsync(id);
    }

    public ValueTask<User?> GetUser(Guid id)
    {
        return  _context.Users.FindAsync(id);
    }

    public Task<User?> GetUserByForeignId(string foreignId)
    {
        return _context.Users.FirstOrDefaultAsync(u  => u.ForeignId == foreignId);
    }

    #endregion

    #region Set

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

    public async Task<Candidate> CreateCandidate(Candidate candidate)
    {
        await _context.Candidates.AddAsync(candidate);
        await _context.SaveChangesAsync();
        return candidate;
    }

    public async Task<Lecture> CreateLecture(Lecture lecture)
    {
        await _context.Lectures.AddAsync(lecture);
        await _context.SaveChangesAsync();
        return lecture;
    } 
    #endregion
}
