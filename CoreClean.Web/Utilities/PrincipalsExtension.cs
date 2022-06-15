using System;
using System.Security.Claims;

namespace CoreClean.Web.Utilities
{
    public static class PrincipalsExtension
    {
        public static Guid GetId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            return new Guid(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}