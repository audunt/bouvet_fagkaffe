using bouvet_fagkaffe_repository;
using bouvet_fagkaffe_repository.Models;
using System.Security.Claims;

namespace bouvet_fagkaffe_frontend;

public class UserHelper(Operations operations)
{
    public Operations Operations { get; } = operations;

    public async Task<User?> GetUser(ClaimsPrincipal? principal)
    {
        if (principal == null)
            throw new Exception("No valid princiapl provided");

        //Get foreignId, nullcheck
        var foreignId = GetForeignId(principal);
        if (foreignId == null)
            return null;

        //If user doesn't exist => Create
        //Else => Update
        var user = await operations.GetUserByForeignId(foreignId);
        if (user == null)
        {
            var newUser = new User()
            {
                ForeignId = foreignId,
                FirstName = GetUserFirstName(principal),
                LastName = GetUserLastName(principal),
                Email = GetUserEmail(principal),
                Groups = GetUserGroups(principal)
            };
            //TODO: perform admin check
            user = await Operations.CreateUser(newUser);
        }
        else
        {
            user.ForeignId = foreignId;
            user.FirstName = GetUserFirstName(principal);
            user.LastName = GetUserLastName(principal);
            user.Email = GetUserEmail(principal);
            user.Groups = GetUserGroups(principal);
            user = await Operations.UpdateUser(user);
        }
        return user;
    }

    #region claims
    public string GetUserFirstName(ClaimsPrincipal principal)
    {
        var firstName = principal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/FirstName")?.Value;
        return firstName ?? throw new Exception("No first name found");
    }

    public string GetUserLastName(ClaimsPrincipal principal)
    {
        var lastName = principal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/LastName")?.Value;
        return lastName ?? throw new Exception("No last name found");
    }

    public string GetUserEmail(ClaimsPrincipal principal)
    {
        var email = principal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Email")?.Value;
        return email ?? throw new Exception("No email found");
    }

    public string GetForeignId(ClaimsPrincipal principal)
    {
        var foreignId = principal.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
        return foreignId ?? throw new Exception("No foreign id found");
    }

    public List<string?> GetUserGroups(ClaimsPrincipal principal)
    {
        List<string?> groups = [];
        var groupClaims = principal.FindAll("http://schemas.microsoft.com/ws/2008/06/identity/claims/role").ToList();
        foreach (var groupClaim in groupClaims)
        {
            groups.Add(groupClaim.Value);
        }
        return groups;
    }
    #endregion
}
