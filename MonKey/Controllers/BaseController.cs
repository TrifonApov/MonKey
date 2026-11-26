namespace MonKey.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class BaseController : Controller
    {
        private string? GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        protected Guid? GetUserGuid()
        {
            return Guid.Parse(GetUserId());
        }
    }
}
