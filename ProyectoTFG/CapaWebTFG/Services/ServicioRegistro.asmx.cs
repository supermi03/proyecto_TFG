using Newtonsoft.Json;
using ProyectoTFG.Modelos;
using System;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using ProyectoTFG.DAL;
using BCrypt.Net;  // Asegúrate de tener esta referencia

namespace CapaWebTFG.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class ServicioRegistro : WebService
    {
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string Registro(string nombre, string apellido, string email, string password, string direccion, string fechaNacimiento, string telefono)
        {
            try
            {
                // Intentar convertir la cadena de fecha en DateTime
                if (!DateTime.TryParse(fechaNacimiento, out DateTime fechaNac))
                {
                    return JsonConvert.SerializeObject(new { success = false, message = "Fecha de nacimiento no es válida." });
                }

                using (var context = new Contexto()) // Asegúrate de que tu contexto de base de datos esté configurado correctamente
                {
                    // Validar que el email no exista
                    var existingUser = context.Personas.FirstOrDefault(p => p.Email == email);
                    if (existingUser != null)
                    {
                        return JsonConvert.SerializeObject(new { success = false, message = "El email ya está registrado." });
                    }

                    // Crear una nueva instancia de Persona
                    var persona = new Persona
                    {
                        Nombre = nombre,
                        Apellido = apellido,
                        Email = email,
                        Password = HashPassword(password), // Hashear la contraseña antes de almacenarla
                        Direccion = direccion,
                        Fecha_nacimiento = fechaNac, // Almacenar como DateTime
                        Telefono = telefono,
                        Tipo_Persona = 1, // Asegúrate de ajustar el tipo de persona según tu lógica
                        Fecha_Registro = DateTime.Now
                    };

                    // Agregar la nueva persona al contexto y guardar los cambios
                    context.Personas.Add(persona);
                    context.SaveChanges();
                }

                // Formatear la fecha en el formato deseado para presentación (si es necesario)
                string fechaFormateada = fechaNac.ToString("yyyy-MM-dd HH:mm:ss.fff");

                return JsonConvert.SerializeObject(new { success = true, fechaNacimiento = fechaFormateada });
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return JsonConvert.SerializeObject(new { success = false, message = ex.Message });
            }
        }

        // Método para hashear la contraseña usando BCrypt
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
