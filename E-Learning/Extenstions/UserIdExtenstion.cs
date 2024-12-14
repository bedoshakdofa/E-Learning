using System.Security.Claims;

namespace E_Learning.Extenstions
{
    public static class UserIdExtenstion
    {
        public static string GetUserId(this ClaimsPrincipal claim)
        {
            return claim.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
