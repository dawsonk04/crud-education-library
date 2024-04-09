using DRK.ProgDec.UI.Extensions;

namespace DRK.ProgDec.UI.Models
{
    public static class Authenticate
    {
        public static bool isAuthenticated(HttpContext context)
        {
            if (context.Session.GetObject<User>("user") != null)
            {
                return true;
            }
            else { return false; }
        }
    }
}
