using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Converge.Shared.Helper
{
    public class ClaimsHelper
    {
        private static IHttpContextAccessor _httpContextAccessor;
        public ClaimsHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static int GetUserId()
        {
            if (_httpContextAccessor?.HttpContext?.User != null)
            {
                var userIdString = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (int.TryParse(userIdString, out int userId))
                {
                    return userId;
                }
            }
            return 0;
        }
    }
}
