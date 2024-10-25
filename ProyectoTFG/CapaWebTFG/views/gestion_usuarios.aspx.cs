using System;
using System.Web;

namespace CapaWebTFG.views
{
    public partial class gestion_usuarios : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica si el usuario está autenticado
            if (!IsUserAuthenticated())
            {
                // Redirige al inicio de sesión si no está autenticado
                Response.Redirect("../views/IniciarSesion.aspx");
            }

            // Aquí puedes añadir el resto de la lógica para cargar la página
        }
    }
}
