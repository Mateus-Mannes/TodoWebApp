using System.Security.Claims;

namespace TodoApp.Extensions;

public static class ClaimsPrincipalExtension
{
    public static int UserId(this ClaimsPrincipal claims)
    {
        try
        {
            var value = claims?.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(value)) throw new Exception("UserId claim not found");
            return int.Parse(value);
        }
        catch
        {
            return 0;
        }
    }
}
