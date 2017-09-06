using System.Security.Claims;
using System.Security.Principal;

namespace lua.Models.Extensions
{
    public static class Extensions
    {
        public static string GetPoints(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Points");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}