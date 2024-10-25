using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace CapaWebTFG.Services
{
    /// <summary>
    /// Descripción breve de CerrarSesion
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class CerrarSesion : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string CerrarSesiones()
        {
            try
            {
                // Eliminar las variables de sesión
                HttpContext.Current.Session.Abandon();

                // También eliminar la cookie si es necesario
                if (HttpContext.Current.Request.Cookies["UserId"] != null)
                {
                    HttpCookie cookie = new HttpCookie("UserId");
                    cookie.Expires = DateTime.Now.AddDays(-1); // Expirar la cookie
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }

                return JsonConvert.SerializeObject(new { success = true, message = "Sesión cerrada correctamente." });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { success = false, message = ex.Message });
            }
        }

    }
}
