using System.Security.Claims;

namespace TodoApp.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static int UserId(this ClaimsPrincipal claims)
        {
            try
            {
                var value = claims?.FindFirst("UserId")?.Value;
                return int.Parse(value);
            }
            catch
            {
                return 0;
            }
        }
    }
}
