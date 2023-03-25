using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DiplomaProject.Controllers
{
    public abstract class ApiBaseController : ControllerBase
    {
        protected long GetCurrentUserId()
        {
            var claimsPrincipal = HttpContext.User as ClaimsPrincipal;

            if (claimsPrincipal == null)
            {
                throw new UnauthorizedAccessException();
            }

            var claimEmployeeId = claimsPrincipal.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (claimEmployeeId == null)
            {
                throw new UnauthorizedAccessException();
            }

            if (!long.TryParse(claimEmployeeId.Value, out long employeeId))
            {
                throw new UnauthorizedAccessException();
            }

            return employeeId;
        }
    }
}
