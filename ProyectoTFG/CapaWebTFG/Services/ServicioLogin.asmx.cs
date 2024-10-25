using Newtonsoft.Json;
using ProyectoTFG.Modelos;
using System;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using ProyectoTFG.DAL;
using BCrypt.Net;

namespace CapaWebTFG.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class ServicioLogin : WebService
    {
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string IniciarSesion(string email, string password, bool recordarme)
        {
            try
            {
                // Crear el contexto
                using (var context = new Contexto())
                {
                    // Buscar al usuario por email
                    var persona = context.Personas.FirstOrDefault(p => p.Email == email);
                    if (persona == null)
                    {
                        return JsonConvert.SerializeObject(new { success = false, message = "El email no está registrado." });
                    }

                    // Verificar la contraseña (compara con la contraseña hasheada)
                    if (!BCrypt.Net.BCrypt.Verify(password, persona.Password))
                    {
                        return JsonConvert.SerializeObject(new { success = false, message = "Contraseña incorrecta." });
                    }

                    // Si la autenticación fue exitosa
                    // Manejar la cookie
                    if (recordarme)
                    {
                        HttpCookie cookie = new HttpCookie("UserId", persona.Persona_Id.ToString())
                        {
                            Expires = DateTime.Now.AddDays(7) // La cookie expira en 7 días
                        };
                        HttpContext.Current.Response.Cookies.Add(cookie);
                    }

                    // Retornar el resultado exitoso
                    return JsonConvert.SerializeObject(new { success = true, message = "Inicio de sesión exitoso.", persona });
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores más detallado
                return JsonConvert.SerializeObject(new { success = false, message = $"Error: {ex.Message}" });
            }
        }
    }
}
