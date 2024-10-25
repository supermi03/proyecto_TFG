using Newtonsoft.Json;
using ProyectoTFG.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using ProyectoTFG.DAL;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Data.Entity.Infrastructure;

namespace CapaWebTFG.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    public class ServicioPersonas : WebService
    {
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string ObtenerPersonas()
        {
            try
            {
                using (var context = new Contexto())
                {
                    var personas = context.Personas
                        .Select(p => new
                        {
                            p.Persona_Id,
                            p.Nombre,
                            p.Apellido,
                            p.Email,
                            p.Telefono,
                            p.Fecha_nacimiento,
                            p.Direccion,
                            TipoPersona = new
                            {
                                p.TipoPersona.Tipo_Persona_Id,
                                p.TipoPersona.Nombre
                            }
                        }).ToList();

                    return JsonConvert.SerializeObject(new { success = true, data = personas });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { success = false, message = ex.Message });
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string BorrarPersona(int id)
        {
            try
            {
                using (var context = new Contexto())
                {
                    var persona = context.Personas.Find(id);
                    if (persona != null)
                    {
                        context.Personas.Remove(persona);
                        context.SaveChanges();
                        return JsonConvert.SerializeObject(new { success = true });
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { success = false, message = "Persona no encontrada." });
                    }
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { success = false, message = ex.Message });
            }
        }

        
        // Método para hashear la contraseña usando BCrypt
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        [WebMethod]
        public string AgregarPersona(string nombre, string apellido, string email, string password, string telefono, string fechaNacimiento, string direccion, int tipoPersona)
        {
            try
            {
                // Validar que todos los campos requeridos no estén vacíos
                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) ||
                    string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(telefono) || string.IsNullOrWhiteSpace(password) ||
                    string.IsNullOrWhiteSpace(fechaNacimiento) || string.IsNullOrWhiteSpace(direccion))
                {
                    return JsonConvert.SerializeObject(new { success = false, message = "Todos los campos son obligatorios." });
                }
                if (!DateTime.TryParseExact(fechaNacimiento, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime fechaParsed))
                {
                    return JsonConvert.SerializeObject(new { success = false, message = "Formato de fecha no válido. Use el formato dd/MM/yyyy." });
                }

                // Validar que el email no exista antes de crear la entidad
                using (var context = new Contexto())
                {
                    if (context.Personas.Any(p => p.Email == email))
                    {
                        return JsonConvert.SerializeObject(new { success = false, message = "El email ya está registrado." });
                    }
                }

                // Crear un nuevo objeto persona
                var nuevaPersona = new Persona
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    Email = email,
                    Telefono = telefono,
                    Direccion = direccion,
                    Fecha_nacimiento = fechaParsed,
                    Password = HashPassword(password), // Usar hashing para la contraseña
                    Fecha_Registro = DateTime.Now,
                    Tipo_Persona = tipoPersona
                };

                // Validar la entidad Persona
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(nuevaPersona);
                if (!Validator.TryValidateObject(nuevaPersona, validationContext, validationResults, true))
                {
                    return JsonConvert.SerializeObject(new { success = false, message = "Validación fallida: " + string.Join(", ", validationResults.Select(v => v.ErrorMessage)) });
                }

                // Añadir la persona a la base de datos
                using (var context = new Contexto())
                {
                    context.Personas.Add(nuevaPersona);
                    context.SaveChanges();
                }

                return JsonConvert.SerializeObject(new { success = true });
            }
            catch (FormatException)
            {
                return JsonConvert.SerializeObject(new { success = false, message = "Formato de fecha no válido. Use el formato dd/MM/yyyy." });
            }
            catch (ArgumentNullException ex)
            {
                return JsonConvert.SerializeObject(new { success = false, message = "Un parámetro requerido es nulo: " + ex.ParamName });
            }
            catch (Exception)
            {
                // Loguear el error en algún sistema de logging para auditoría
                return JsonConvert.SerializeObject(new { success = false, message = "Ha ocurrido un error inesperado. Por favor, intenta nuevamente." });
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string EditarPersona(int persona_Id, string Nombre, string Apellido, string Email, string Telefono, DateTime FechaNacimiento, string Direccion)
        {
            try
            {
                using (var context = new Contexto())
                {
                    // Buscar la persona por su ID
                    var persona = context.Personas.Find(persona_Id);
                    if (persona != null)
                    {
                        // Actualizar los campos de la persona
                        persona.Nombre = Nombre; // Cambiar a "Nombre"
                        persona.Apellido = Apellido; // Cambiar a "Apellido"
                        persona.Email = Email; // Cambiar a "Email"
                        persona.Telefono = Telefono; // Cambiar a "Telefono"
                        persona.Fecha_nacimiento = FechaNacimiento; // Cambiar a "FechaNacimiento"
                        persona.Direccion = Direccion; // Cambiar a "Direccion"

                        // Guardar los cambios en la base de datos
                        context.SaveChanges();

                        return JsonConvert.SerializeObject(new { success = true });
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { success = false, message = "Persona no encontrada." });
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                // Manejo específico para excepciones de actualización
                return JsonConvert.SerializeObject(new { success = false, message = "Error al actualizar la base de datos: " + dbEx.Message });
            }
            catch (Exception ex)
            {
                // Manejo genérico de excepciones
                return JsonConvert.SerializeObject(new { success = false, message = "Error: " + ex.Message });
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string ObtenerPersonaPorId(int id)
        {
            try
            {
                using (var context = new Contexto())
                {
                    var persona = context.Personas.Find(id);
                    if (persona != null)
                    {
                        return JsonConvert.SerializeObject(new { success = true, data = persona });
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { success = false, message = "Persona no encontrada." });
                    }
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { success = false, message = ex.Message });
            }
        }


    }
}
