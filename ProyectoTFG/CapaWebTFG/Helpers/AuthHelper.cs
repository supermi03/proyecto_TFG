using System;
using System.Web;

public static class AuthHelper
{
    public static bool IsUserAuthenticated()
    {
        return HttpContext.Current.Request.Cookies["UserId"] != null; // O la manera en que almacenes la sesión
    }
}
