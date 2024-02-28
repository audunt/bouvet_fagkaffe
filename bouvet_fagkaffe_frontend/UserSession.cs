using bouvet_fagkaffe_repository;
using System.Security.Claims;

namespace bouvet_fagkaffe_frontend;

public class UserSession(IHttpContextAccessor contextAccessor, Operations operations)
{
    public IHttpContextAccessor ContextAccessor { get; } = contextAccessor;
    public Operations Operations { get; } = operations;


    public string? GetUserName()
    {
        var principal = ContextAccessor.HttpContext?.User;
        if (principal == null)
            return null;
        var firstName = principal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/FirstName")?.Value;
        var lastName = principal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/LastName")?.Value;
        return firstName + " " + lastName;
    }

    public string? GetUserEmail()
    {
        var principal = ContextAccessor.HttpContext?.User;
        if (principal == null)
            return null;
        return principal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Email")?.Value;
    }

    public List<Claim>? GetUserGroups()
    {
        var principal = ContextAccessor.HttpContext?.User;
        if(principal == null)
            return null;
        return principal.FindAll("http://schemas.microsoft.com/ws/2008/06/identity/claims/role").ToList();
    }

    public string? GetUserId()
    {
        var principal = ContextAccessor.HttpContext?.User;
        if (principal == null)
            return null;
        return principal.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
    }
}
