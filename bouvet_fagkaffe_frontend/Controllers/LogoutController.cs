using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bouvet_fagkaffe_frontend.Controllers;

public class LogoutController : Controller
{
    [Route("/logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Ok("Logged Out");
    }
}
