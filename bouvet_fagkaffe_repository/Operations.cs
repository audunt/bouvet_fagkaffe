using bouvet_fagkaffe_repository.Context;
using bouvet_fagkaffe_repository.Models;

namespace bouvet_fagkaffe_repository;

public class Operations(FagkaffeContext context)
{
    private readonly FagkaffeContext _context = context;

    #region Get
    public ValueTask<Candidate?> GetCandidates(Guid id)
    {
        return _context.Candidates.FindAsync(id);
    }

    public ValueTask<Lecture?> GetLectures(Guid id)
    {
        return _context.Lectures.FindAsync(id);
    }

    public ValueTask<User?> GetUsers(Guid id)
    {
        return  _context.Users.FindAsync(id);
    }

    #endregion

    #region Set

    public async Task<User> CreateUser(string ForeignId, string FirstName, string LastName, string Email, List<string?> Groups)
    {

        //TODO: Implement logic to decide if user is admin or not.
        var user = new User
        {
            ForeignId = ForeignId,
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Groups = Groups
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<Candidate> CreateCandidate()
    {
        throw new NotImplementedException();
    }

    #endregion
}
