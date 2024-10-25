using System;
using System.Web;

public class BasePage : System.Web.UI.Page
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsUserAuthenticated())
        {
            Response.Redirect("../views/IniciarSesion.aspx");
        }
    }

    public bool IsUserAuthenticated()
    {
        return HttpContext.Current.Request.Cookies["UserId"] != null;
    }
}
